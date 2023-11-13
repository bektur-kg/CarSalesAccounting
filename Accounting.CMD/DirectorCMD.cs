using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models;
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
        9 - Add a new car
        Q - Quit
        ";

        public string Login { get; private set; }

        public DirectorCMD(string login)
        {
            Login = login;
        }

        public void CommandsList()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nPlease enter a number of a command in menu.\nIf you finished enter 9");
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
                        Console.WriteLine("Creating a new employee\n");
                        CreateNewUser();

                        break;
                    case ConsoleKey.D9:

                        break;
                    case ConsoleKey.Q:
                        break;
                }
            }
        }

        private void CreateNewUser()
        {
            string login = ConsoleInput.Text("Login: ");
            string password = ConsoleInput.Text("Password: ");
            string accountType = ConsoleInput.Text("AccountType (Seller, Repairman): ");

            if (accountType == AccountTypesEnum.Seller.ToString() || accountType == AccountTypesEnum.Repairman.ToString() || accountType == AccountTypesEnum.Director.ToString())
            {
                Enum.TryParse(accountType, out AccountTypesEnum accountTypeParsed);

                UserController userController = new UserController(Login);

                userController.CreateNewUser(login, password, accountTypeParsed);

                Console.WriteLine($"New user: {login} created successfully. The user type is {accountTypeParsed}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, but there is no such account type. Try again!");
            }

        }
    }
}
