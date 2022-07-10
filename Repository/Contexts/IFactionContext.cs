namespace Repository.Contexts
{
    public interface IFactionContext
    {
        Task<int> SaveAsync(string name);
    }
}