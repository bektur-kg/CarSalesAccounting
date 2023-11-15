using System;

namespace Accounting.BL.Helpers
{
    public static class ConsoleOutput
    {
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
