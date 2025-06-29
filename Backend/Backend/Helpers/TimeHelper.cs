namespace Backend.Helpers
{
    public static class TimeHelper
    {
        public static int GetReminingTimeInSeconds(DateTime startTime, int durationInSeconds)
        {
            var elapsed = (DateTime.UtcNow - startTime).TotalSeconds;   // Time passed since the start
            var remaining = durationInSeconds - elapsed;    // Remaining time
            return (int)Math.Max(remaining, 0); // Avoid negative numbers
        }
    }
}
