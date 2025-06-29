namespace Backend.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random _random = new Random();

        public static int Generate(int min, int max)
        {
            return _random.Next(min, max + 1); // inclusive upper bound
        }
    }
}
