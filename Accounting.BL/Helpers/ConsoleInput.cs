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
                    ConsoleOutput.ErrorMessage($"Sorry, but {inputName} can't be empty");

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
                    ConsoleOutput.ErrorMessage($"Sorry, but {inputName} can't be empty");

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
                        ConsoleOutput.ErrorMessage($"Sorry, but there's no such type in {inputName}");
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
                    ConsoleOutput.ErrorMessage($"Sorry, but {inputName} can't be empty");

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
                        ConsoleOutput.ErrorMessage($"Sorry, the {inputName} is lower that 0 or not valid data for {inputName}");
                    }
                }
            }
        }

        public static DateTime DateType(string inputName)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(inputName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(value))
                {
                    ConsoleOutput.ErrorMessage($"Sorry, but {inputName} can't be empty");

                    continue;
                }
                else
                {
                    if (DateTime.TryParse(value, out DateTime parsedDateTime))
                    {
                        return parsedDateTime;
                    }
                    else
                    {
                        ConsoleOutput.ErrorMessage($"Sorry, the {value} is incorrect date type");
                    }
                }
            }
        }

        public static bool AcceptAction()
        {
            while (true)
            {
                Console.Write("Are you sure to make this action? (Y/N): ");
                Console.WriteLine("\n");

                ConsoleKeyInfo answer = Console.ReadKey();

                switch (answer.Key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                    default:
                        continue;
                }
            }
        }
    }
}
