namespace MinimalWarhammerRest.Models
{
    public partial class Figure
    {
        public int FigureId { get; set; }
        public string FigureName { get; set; } = null!;
        public int Amount { get; set; }
        public int FactionId { get; set; }
        public int SubfactionId { get; set; }

        public virtual Faction Faction { get; set; } = null!;
        public virtual Faction Subfaction { get; set; } = null!;
    }
}