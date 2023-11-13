using System;

namespace Accounting.BL.Helpers
{
    public static class ConsoleInput
    {
        public static string TextType(string inputName)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(inputName);
                Console.ForegroundColor = ConsoleColor.Cyan;
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

        public static T EnumType<T>(string inputName) where T : struct
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(inputName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Sorry, but {inputName} can't be empty");
                    continue;
                }
                else
                {
                    if (Enum.TryParse(value, out T parsedValue))
                    {
                        return parsedValue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Sorry, but there's no such type in {inputName}");
                        continue;
                    }
                }
            }
        }

        public static double PriceType(string inputName)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(inputName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Sorry, but {inputName} can't be empty");

                    continue;
                }
                else
                {
                    if(double.TryParse(value, out double parsedPrice) && parsedPrice > 0)
                    {
                        return parsedPrice;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sorry, the price is lower that 0 or not valid data for price");
                    }
                }
            }
        }
    }
}
