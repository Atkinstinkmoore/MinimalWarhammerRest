using Microsoft.EntityFrameworkCore;

namespace MinimalWarhammerRest.Models
{
    public class Query
    {
        public IQueryable<Figure> GetFigures([Service] WarhammerDBContext context) =>
            context.Figures.AsNoTracking();
    }

}
