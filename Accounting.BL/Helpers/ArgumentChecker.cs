using System;

namespace Accounting.BL.Helpers
{
    public static class ArgumentChecker
    {
        public static void ArgumentNullChecker(params string[] strings)
        {
            foreach (var item in strings)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new ArgumentNullException($"Item of {strings} can't be empty of null");
                }
            }
        }

        public static void ArgumentNullChecker<T>(params T[] value) where T : class
        {
            foreach (var item in value)
            {
                if (item == null)
                {
                    throw new ArgumentNullException($"Item of {value} can't be or null");
                }
            }
        }

        public static void CheckIfEnumIsAssigned<T>(params T[] enums) where T: Enum
        {
            foreach (var item in enums)
            {
                if (!Enum.IsDefined(typeof(T), item))
                {
                    throw new ArgumentNullException($"Enum is null", nameof(enums));
                }
            }
        }

        public static void CheckPrice(params double[] value)
        {
            foreach (var item in value)
            {
                if (item < 0)
                {
                    throw new Exception($"One of the items of {nameof(value)} is <= 0");
                }
            }
        }
    }
}
