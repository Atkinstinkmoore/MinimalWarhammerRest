namespace MinimalWarhammerRest.Factions
{
    public sealed record FactionDTO(int Id, string Name);
    public sealed record FactionDetailsDTO(int Id, string Name, int AmountOfModels);
}
