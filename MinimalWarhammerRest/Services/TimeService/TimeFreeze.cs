namespace MinimalWarhammerRest.Services.TimeService
{
    public sealed class TimeFreeze : ITimeService
    {
        private readonly DateTimeOffset _time;

        public TimeFreeze(ITimeService timeService)
        {
            _time = timeService.GetCurrentTime();
        }

        public DateTimeOffset GetCurrentTime() => _time;
    }
}
