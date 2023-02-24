using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using MinimalWarhammerRest.Domain.DTOs;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Models;

namespace MinimalWarhammerRest.Services;

public sealed class FactionService : IFactionService
{
    private readonly WarhammerDBContext _context;

    public FactionService(WarhammerDBContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Create(string name)
    {
        var exists =  await _context.Factions.SingleOrDefaultAsync(f => f.FactionName == name);

        if (exists is not null) return false;

        var faction = new Faction() { FactionName = name };
        var result = _context.Factions.Add(faction);
        var _ =  await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var exists = await _context.Factions.FindAsync(id);

        if (exists is not null)
        {
            var _ = _context.Remove(exists);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return true;
        }

        return false;
    }

    public async Task<Result<FactionDTO>> Get(int id)
    {
        var result = await _context.Factions.FindAsync(id);

        if (result is not null)
        {
            var faction = new FactionDTO(result.FactionId, result.FactionName);
            return new Result<FactionDTO>(faction);
        }
        var ex = new FactionNotFoundException(id);
        return new Result<FactionDTO>(ex);
    }

    public async Task<IEnumerable<FactionDTO>> GetAll()
    {
        await _context.Factions.LoadAsync();

        return _context.Factions.Select(f => new FactionDTO(f.FactionId, f.FactionName));
    }
}