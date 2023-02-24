namespace MinimalWarhammerRest.Domain.DTOs
{
    public sealed record MiniatureDTO(int Id, string Name, int Amount, string Faction, string Subfaction);
}
