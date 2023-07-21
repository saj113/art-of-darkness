using Utilities;

namespace Stats
{
    internal static class StatsLevelIncreaser
    {
        public static int IncreaseHealth(int current, int level)
        {
            return Increase(GlobalSettings.IncreaseStatsPercentAmounts.Health, current, level);
        }

        public static int IncreasePower(int current, int level)
        {
            return Increase(GlobalSettings.IncreaseStatsPercentAmounts.Power, current, level);
        }

        private static int Increase(int percentAmount, int current, int level)
        {
            var amount = current * percentAmount / 100;
            for (var i = 1; i < level; i++)
            {
                current += amount;
            }

            return current;
        }
    }
}
