using System;

namespace Accounting.CMD
{
    public class DirectorCMD
    {
        public const string COMMANDS_LIST = @"
        1 - Show list of cars available to sell
        2 - Show quantities of sold cars
        3 - Show a car with maximum count of sells
        4 - Show a car with minimum count of sells
        5 - Show a car that needs service the most time
        6 - Show the most expensive car
        7 - Show the cheapest car
        8 - Create a new employee
        9 - Quit
        ";

        public void CommandsList()
        {
            Console.WriteLine("Please enter a number of a command in menu.\nIf you finished enter 9");
            Console.WriteLine(COMMANDS_LIST);

            //ConsoleKey.D1 is the 1 command
        }
    }
}
