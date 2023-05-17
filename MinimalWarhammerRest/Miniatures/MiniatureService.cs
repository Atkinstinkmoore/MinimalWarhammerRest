using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Models;

namespace MinimalWarhammerRest.Miniatures;

public sealed class MiniatureService : IMiniatureService
{
    private readonly WarhammerDBContext _context;

    public MiniatureService(WarhammerDBContext context)
    {
        _context = context;
    }

    public async Task<Result<MiniatureDTO>> Create(CreateMiniatureRequest req)
    {
        var exists = _context.Figures.SingleOrDefault(m => m.FigureName.ToUpper() == req.Name.ToUpper());

        if (exists is not null || !(req.Amount > 0))
        {
            return new Result<MiniatureDTO>(new CouldNotCreateException(nameof(Faction)));
        }

        await _context.Factions.LoadAsync();

        var factionIds = _context.Factions.Select(f => f.FactionId);
        if (factionIds.Contains(req.FactionId) && factionIds.Contains(req.SubfactionId))
        {
            var toSave = new Figure() { FigureName = req.Name, Amount = req.Amount, FactionId = req.FactionId, SubfactionId = req.SubfactionId };
            var result = _context.Figures.Add(toSave);
            var _ = await _context.SaveChangesAsync();
            return new Result<MiniatureDTO>(new MiniatureDTO(result.Entity.FigureId,
                                                             result.Entity.FigureName,
                                                             result.Entity.Amount,
                                                             result.Entity.Faction.FactionName,
                                                             result.Entity.Subfaction.FactionName));
        }
        return new Result<MiniatureDTO>(new CouldNotCreateException(nameof(Faction)));
    }

    public async Task<Result<MiniatureDTO>> Get(int id)
    {
        var result = await _context.Figures.FindAsync(id);

        if (result is not null)
        {
            var mini = new MiniatureDTO(result.FigureId, result.FigureName, result.Amount, result.Faction.FactionName, result.Subfaction.FactionName);
            return new Result<MiniatureDTO>(mini);
        }
        var ex = new MiniatureNotFoundException(id);
        return new Result<MiniatureDTO>(ex);
    }

    public async Task<IEnumerable<MiniatureDTO>> GetAll()
    {
        await _context.Figures.LoadAsync();

        return _context.Figures.Select(m => new MiniatureDTO(m.FigureId, m.FigureName, m.Amount, m.Faction.FactionName, m.Subfaction.FactionName));
    }
}