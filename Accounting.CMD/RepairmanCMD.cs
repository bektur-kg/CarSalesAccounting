using System;

namespace Accounting.CMD
{
    public class RepairmanCMD
    {
        public const string COMMANDS_LIST = @"
            1 - Get a car to a service
            2 - Show a list of in-service cars
            3 - Finish servicing a car
            4 - Show a list of served cars
            5 - Show my profit
            6 - Quit
        ";

        public void CommandsList()
        {
            Console.WriteLine("Please enter a number of a command in menu.\nIf you finished enter 6");
            Console.WriteLine(COMMANDS_LIST);

            //ConsoleKey.D1 is the 1 command
        }
    }
}
