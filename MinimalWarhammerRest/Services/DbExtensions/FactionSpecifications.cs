using MinimalWarhammerRest.Models;

namespace MinimalWarhammerRest.Services.DbExtensions
{
    public static class FactionSpecifications
    {
        public static IQueryable<Faction> WithPagination(
            this IQueryable<Faction> collection , int page, int pageSize) =>
            collection.Skip((page - 1) * pageSize).Take(pageSize);
    }
}