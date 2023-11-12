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

    }
}
