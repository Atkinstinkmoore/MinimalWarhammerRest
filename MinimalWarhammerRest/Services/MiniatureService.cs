using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Domain.DTOs;
using MinimalWarhammerRest.Domain.Exceptions;
using MinimalWarhammerRest.Domain.Requests;
using MinimalWarhammerRest.Models;

namespace MinimalWarhammerRest.Services
{
    public class MiniatureService : IMiniatureService
    {
        private readonly WarhammerDBContext _context;

        public MiniatureService(WarhammerDBContext context)
        {
            _context = context;
        }

        public async Task<LanguageExt.Common.Result<bool>> Create(CreateMiniatureRequest req)
        {
            var exists = _context.Figures.SingleOrDefault(m => m.FigureName.ToUpper() == req.Name.ToUpper());

            if (exists is not null || !(req.Amount > 0)) return false;

            await _context.Factions.LoadAsync();

            var factionIds = _context.Factions.Select(f => f.FactionId);
            if(factionIds.Contains(req.FactionId) && factionIds.Contains(req.SubfactionId))
            {
                var toSave = new Figure() { FigureName = req.Name, Amount = (int)req.Amount, FactionId = req.FactionId, SubfactionId = req.SubfactionId };
                _context.Figures.Add(toSave);
                var _ = await _context.SaveChangesAsync();
                return new LanguageExt.Common.Result<bool>(true);
            }
            var ex = new CouldNotCreateException(nameof(Faction));
            return new LanguageExt.Common.Result<bool>(ex);

        }

        public async Task<LanguageExt.Common.Result<MiniatureDTO>> Get(int id)
        {
            var result = await _context.Figures.FindAsync(id);

            if (result is not null)
            {
                await _context.Factions.LoadAsync();
                var mini = new MiniatureDTO(result.FigureId, result.FigureName, result.Amount, result.Faction.FactionName, result.Subfaction.FactionName);
                return new LanguageExt.Common.Result<MiniatureDTO>(mini);
            }
            var ex = new MiniatureNotFoundException(id);
            return new LanguageExt.Common.Result<MiniatureDTO>(ex);

        }

        public async Task<IEnumerable<MiniatureDTO>> GetAll()
        {
            await _context.Figures.LoadAsync();
            await _context.Factions.LoadAsync();

            return _context.Figures.Select(m => new MiniatureDTO(m.FigureId, m.FigureName, m.Amount, m.Faction.FactionName, m.Subfaction.FactionName));
        }


    }
}
