using System;

namespace Accounting.CMD
{
    public class SellerCMD
    {
        private const string COMMANDS_LIST = 
        "\n1 - Show all list of cars\n2 - Search a car\n3 - Report of cars\n4 - Order a car\n5 - Show list of sold cars\n6 - Return a car\n8 - Exit\n";

        public void CommandsList()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nPlease enter a number of a command in menu.\nIf you finished enter 7");
                Console.WriteLine(COMMANDS_LIST);

                ConsoleKeyInfo consoleKey = Console.ReadKey();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;


                switch (consoleKey.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("list of all cars");
                        break;
                    case ConsoleKey.D2:

                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        break;
                    case ConsoleKey.D5:
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D7:
                        break;
                    case ConsoleKey.D8:
                        break;
                }
            }
        }
    }
}
