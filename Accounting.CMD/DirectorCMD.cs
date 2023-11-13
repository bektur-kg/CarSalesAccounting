using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using System;
using System.IO;

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
                        Console.WriteLine("Adding a new car\n");
                        AddNewCar();

                        break;
                    case ConsoleKey.Q:
                        break;
                }
            }
        }

        private void CreateNewUser()
        {
            string login = ConsoleInput.TextType("Login: ");
            string password = ConsoleInput.TextType("Password: ");
            string accountType = ConsoleInput.TextType("AccountType (Seller, Repairman, Director): ");

            if (
                accountType == AccountTypesEnum.Seller.ToString() || 
                accountType == AccountTypesEnum.Repairman.ToString() || 
                accountType == AccountTypesEnum.Director.ToString()
                )
            {
                Enum.TryParse(accountType, out AccountTypesEnum parsedAccountType);

                UserController userController = new UserController(Login);

                userController.CreateNewUser(login, password, parsedAccountType);

                Console.WriteLine($"New user: {login} created successfully. The user type is {parsedAccountType}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, but there is no such account type. Try again!");
            }

        }

        private void AddNewCar()
        {
            string brand = ConsoleInput.TextType("Brand: ");
            string model = ConsoleInput.TextType("Model: ");
            CarBodyTypesEnum bodyType = ConsoleInput.EnumType<CarBodyTypesEnum>("Body Type of a Car: ");
            ATTEnum ATT = ConsoleInput.EnumType<ATTEnum>("Automatic Transmission Type: ");
            FuelTypeEnum fuelType = ConsoleInput.EnumType<FuelTypeEnum>("Fuel Type: ");
            double price = ConsoleInput.PriceType("Price: ");
            string description = ConsoleInput.TextType("Description: ");

            CarsController carsController = new CarsController(Login);

            try
            {
                carsController.AddCar(model, brand, bodyType, ATT, price, description, fuelType);

                Console.WriteLine($"A new car Brand: {brand}, Model: {model}, Price: {price}$ successfully added");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your account type isn't Director");
            }
        }
    }
}
