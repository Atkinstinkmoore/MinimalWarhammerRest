namespace Repository.Models
{
    internal class Figure
    {
        public int FigureId { get; set; }
        public string FigureName { get; set; }
        public int Amount { get; set; }
        public int FactionId { get; set; }
        public int SubFactionId { get; set; }
    }
}
