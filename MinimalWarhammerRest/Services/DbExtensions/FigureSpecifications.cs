using MinimalWarhammerRest.Models;

namespace MinimalWarhammerRest.Services.DbExtensions
{
    internal static class FigureSpecifications
    {
        public static IQueryable<Figure> WithPagination(
            this IQueryable<Figure> collection, int page, int pageSize) =>
            collection.Skip((page - 1) * pageSize).Take(pageSize);
    }
}