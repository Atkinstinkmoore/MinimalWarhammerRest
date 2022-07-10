namespace Repository.Contexts
{
    public class FactionContext : IFactionContext
    {
        public async Task<int> SaveAsync(string name)
        {
            Task.Delay(100).Wait();
            return 1;
        }
    }
}
