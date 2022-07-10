namespace MinimalWarhammerRest.Domain.Exceptions
{
    public sealed class MiniatureNotFoundException : Exception
    {
        public MiniatureNotFoundException(int id) : base($"Miniature with id {id} not found")
        {
        }
    }
}
