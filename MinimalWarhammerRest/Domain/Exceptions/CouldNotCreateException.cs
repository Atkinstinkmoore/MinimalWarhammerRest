namespace MinimalWarhammerRest.Domain.Exceptions
{
    public sealed class CouldNotCreateException : Exception
    {
        public CouldNotCreateException(string typeOf) : base($"Could not create {typeOf}")
        {
        }
    }
}
