using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using System;
using BetterConsoleTables;
using System.Collections.Generic;
using Accounting.BL.Exceptions;

namespace Accounting.CMD
{
    public class DirectorCMD
    {
        //todo: make a string with beautiful indentations
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
        Q - Quit";

        public string Login { get; private set; }
        public CarsController CarsController { get; set; }

        public DirectorCMD(string login)
        {
            Login = login;
            CarsController = new CarsController(Login);
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
                        Console.WriteLine("List of all cars");

                        WriteCarsTable(CarsController.Cars);
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
                        Console.WriteLine("The Most Expensive Car is: ");
                        GetPriciestCar();

                        break;
                    case ConsoleKey.D7:
                        Console.WriteLine("The Cheapest Car is: ");
                        GetCheapestCar();

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
            double userCommissionPercents = 0.0;

            if (accountType == AccountTypesEnum.Seller.ToString())
            {
                userCommissionPercents = ConsoleInput.PriceType("User Commission Percents: ");
            }

            if (
                accountType == AccountTypesEnum.Seller.ToString() || 
                accountType == AccountTypesEnum.Repairman.ToString() || 
                accountType == AccountTypesEnum.Director.ToString()
                )
            {
                Enum.TryParse(accountType, out AccountTypesEnum parsedAccountType);

                UserController userController = new UserController(Login);

                try
                {
                    userController.CreateNewUser(login, password, parsedAccountType, userCommissionPercents);
                }
                catch (AccountIsTakenException)
                {
                    ConsoleOutput.ErrorMessage($"Sorry, This login - {login} is already taken. Try again!");
                    return;
                }

                Console.WriteLine($"New user: {login} created successfully. The user type is {parsedAccountType}");
            }
            else
            {
                ConsoleOutput.ErrorMessage("Sorry, but there is no such account type. Try again!");
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

            try
            {
                CarsController.AddCar(model, brand, bodyType, ATT, price, description, fuelType);

                Console.WriteLine($"A new car Brand: {brand}, Model: {model}, Price: {price}$ successfully added");
            }
            catch (Exception)
            {
                ConsoleOutput.ErrorMessage("Your account type isn't Director");
            }
        }

        private void WriteCarsTable(List<Car> Cars)
        {
            Table table = new Table("#", "Brand", "Model", "Body Type", "ATT", "Price ($)", "Fuel Type", "Description") { Config = TableConfiguration.MySql() };

            for (int i = 0; i < Cars.Count; i++)
            {
                var (model, brand, bodyType, ATT, fuelType, price, description) = Cars[i].GetCarValues();
                
                table.AddRow(i + 1, model, brand, bodyType, ATT, price, fuelType, description);
            }

            Console.Write(table.ToString());
        }

        private void GetPriciestCar()
        {
            Car priciestCar = CarsController.GetPriciestCar();

            if (priciestCar != null)
            {
                List<Car> carsList = new List<Car> { priciestCar };

                WriteCarsTable(carsList);
            }
            else
            {
                Console.WriteLine("The list of cars is empty");
            }
        }

        private void GetCheapestCar()
        {
            Car cheapestCar = CarsController.GetCheapestCar();

            if (cheapestCar != null)
            {
                List<Car> carsList = new List<Car> { cheapestCar };

                WriteCarsTable(carsList);
            }
            else
            {
                Console.WriteLine("The list of cars is empty");
            }
        }
    }
}
