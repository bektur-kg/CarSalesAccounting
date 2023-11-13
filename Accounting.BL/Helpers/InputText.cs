using System;

namespace Accounting.BL.Helpers
{
    public static class ConsoleInput
    {
        public static string Text(string inputName)
        {
            while (true)
            {   
                Console.Write(inputName);
                string value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Sorry, but {inputName} can't be empty");

                    continue;
                }
                else
                {
                    return value;
                }
            }
        }
    }
}
