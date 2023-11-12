using System;

namespace Accounting.CMD
{
    public class SellerCMD
    {
        private const string COMMANDS_LIST = @"
        1 - Show all list of cars
        2 - Search a car
        3 - Report of cars
        4 - Order a car
        5 - Show list of sold cars
        6 - Return a car
        8 - Exit
        ";

        public void CommandsList()
        {
            while (true)
            {
                Console.WriteLine("Please enter a number of a command in menu.\nIf you finished enter 7");
                Console.WriteLine(COMMANDS_LIST);

                ConsoleKeyInfo consoleKey = Console.ReadKey();

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
            //ConsoleKey.D1 is the 1 command
        }
    }
}
