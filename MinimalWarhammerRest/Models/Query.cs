using Microsoft.EntityFrameworkCore;
using MinimalWarhammerRest.Domain.DTOs;
using MinimalWarhammerRest.Services;

namespace MinimalWarhammerRest.Models
{
    public class Query
    {
        public async Task<IQueryable<MiniatureDTO>> GetMinis([Service] IMiniatureService service)
        {
            var figures = await service.GetAll();
            return figures.AsQueryable();
        }
        public async Task<MiniatureDTO?> GetMiniById([Service] IMiniatureService service, int id)
        {
            var figures = await service.Get(id);

            return figures.Match(s =>
            {
                return s;
            }, ex =>
            {
                return null;
            });
        }
    }

}
