namespace MinimalWarhammerRest.Services.TimeService
{
    public sealed class TimeHelper : ITimeService
    {
        public DateTimeOffset GetCurrentTime() => DateTimeOffset.UtcNow;
    }
}
