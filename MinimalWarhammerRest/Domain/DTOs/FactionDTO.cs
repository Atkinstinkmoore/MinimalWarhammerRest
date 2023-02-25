namespace MinimalWarhammerRest.Domain.DTOs
{
    public sealed record FactionDTO(int Id, string Name);
    public sealed record FactionDetailsDTO(int Id, string Name, int AmountOfModels);
}
