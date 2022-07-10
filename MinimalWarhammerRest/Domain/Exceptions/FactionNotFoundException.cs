namespace MinimalWarhammerRest.Domain.Exceptions
{
    public sealed class FactionNotFoundException : Exception
    {
        public FactionNotFoundException(int id) : base($"Faction with ID {id} not found")
        {
        }
    }
}
