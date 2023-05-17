using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Models;

namespace MinimalWarhammerRest.Factions;

public sealed class FactionService : IFactionService
{
    private readonly WarhammerDBContext _context;

    public FactionService(WarhammerDBContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Create(string name)
    {
        var exists = await _context.Factions.SingleOrDefaultAsync(f => f.FactionName == name);

        if (exists is not null) return false;

        var faction = new Faction() { FactionName = name };
        var result = _context.Factions.Add(faction);
        var saved = await _context.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var exists = await _context.Factions.FindAsync(id);

        if (exists is null) return false;

        var _ = _context.Remove(exists);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Result<FactionDetailsDTO>> Get(int id)
    {
        var result = await _context.Factions.FindAsync(id);

        if (result is not null)
        {
            var count = _context.Figures.Where(m => m.FactionId == id || m.SubfactionId == id).Sum(m => m.Amount);
            var faction = new FactionDetailsDTO(result.FactionId, result.FactionName, count);
            return new Result<FactionDetailsDTO>(faction);
        }
        var ex = new FactionNotFoundException(id);
        return new Result<FactionDetailsDTO>(ex);
    }

    public async Task<IEnumerable<FactionDTO>> GetAll()
    {
        await _context.Factions.LoadAsync();

        return _context.Factions.Select(f => new FactionDTO(f.FactionId, f.FactionName));
    }
}