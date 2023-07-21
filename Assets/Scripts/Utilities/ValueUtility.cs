using Core;
using Stats;
using UnityEngine;
using Random = System.Random;

namespace Utilities
{
    public static class ValueUtility
    {
        private static readonly Random _random = new Random();

        public static int GetDirection(ICharacteristics characteristics)
        {
            return characteristics.IsFacingRight ? 1 : -1;
        }

        public static int GetDirection(Vector2 fromPosition, Vector2 toPosition)
        {
            return GetDirection(fromPosition.x, toPosition.x);
        }

        public static int GetDirection(float fromPosition, float toPosition)
        {
            return fromPosition > toPosition ? -1 : 1;
        }

        public static int GetDirection(ICharacteristics characteristics, Direction direction)
        {
            if (direction == Direction.Round)
            {
                return 0;
            }

            var directionAsInt = GetDirection(characteristics);
            directionAsInt *= direction == Direction.Back ? -1 : 1;

            return directionAsInt;
        }

        public static int CalculatePercent(int value, int percent)
        {
            return value*percent/100;
        }

        public static float CalculatePercent(float value, float percent)
        {
            return value * percent / 100.0f;
        }

        public static float GetRandom(float minimum, float maximum)
        {
            return (float)_random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static int GetRandom(int from, int to)
        {
            if (from < to)
            {
                return _random.Next(from, to);
            }

            return _random.Next(to, from);
        }

        public static int GetRollbackAmout(int value, int power)
        {
            var rate = power / 100;
            return (value * rate - value) * -1;
        }
    }
}
