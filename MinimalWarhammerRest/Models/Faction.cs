namespace MinimalWarhammerRest.Models
{
    public partial class Faction
    {
        public Faction()
        {
            FigureFactions = new HashSet<Figure>();
            FigureSubfactions = new HashSet<Figure>();
        }

        public int FactionId { get; set; }
        public string FactionName { get; set; } = null!;

        public virtual ICollection<Figure> FigureFactions { get; set; }
        public virtual ICollection<Figure> FigureSubfactions { get; set; }
    }
}