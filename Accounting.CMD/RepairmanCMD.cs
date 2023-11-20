using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using BetterConsoleTables;
using System;
using System.Collections.Generic;

namespace Accounting.CMD
{
    public class RepairmanCMD
    {
        public const string COMMANDS_LIST = @"
            1 - Get a car to a service
            2 - Show a list of in-service cars
            3 - Finish servising a car
            4 - Show a list of served cars
            5 - Show my profit
            Q - Quit
        ";

        public CarsController CarsController { get; private set; }
        public string Login { get; private set; }

        public RepairmanCMD(string login)
        {
            CarsController = new CarsController(login);
            Login = login;
        }

        public void CommandsList()
        {
            while (true)
            {
                Console.WriteLine("Please enter a number of a command in menu.\nIf you finished enter Q");
                Console.WriteLine(COMMANDS_LIST);

                ConsoleKeyInfo consoleKey = Console.ReadKey();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;

                switch (consoleKey.Key)
                {
                    case ConsoleKey.D1:
                        GetCarToService();

                        break;
                    case ConsoleKey.D2:
                        CarsTable(CarsController.InServiceCars, "\t\t\tThe list of In-Service Cars");

                        break;
                    case ConsoleKey.D3:
                        FinishServingCar();

                        break;
                    case ConsoleKey.D4:
                        CarsTable(CarsController.ServedCars, "\t\t\tThe list of Served Cars");

                        break;
                    case ConsoleKey.D5:
                        ShowProfit();

                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        ConsoleOutput.WarningMessage("There is no such command");
                        break;
                }
            }
        }

        private void FinishServingCar()
        {
            string brandName = ConsoleInput.TextType("Car Brand: ");
            string modelName = ConsoleInput.TextType("Car Model: ");
            double workPrice = ConsoleInput.PriceType("Price for work: ");

            var (servedCar, userCommissionPercents, userCommissionPrice) = CarsController.FinishCarService(brandName, modelName, workPrice);

            if (servedCar != null)
            {
                ConsoleOutput.InfoMessage($"The {servedCar.Brand} {servedCar.Model} has been serviced successfully.\nRepairman commission is {userCommissionPercents}% or {userCommissionPrice}$");
            }
            else
            {
                ConsoleOutput.ErrorMessage($"Sorry, there is no such car in service list - {brandName} {modelName}"); 
            }
        }

        private void GetCarToService()
        {
            string brandName = ConsoleInput.TextType("Car Brand: ");
            string modelName = ConsoleInput.TextType("Car Model: ");
            string serviceReason = ConsoleInput.TextType("The reason why this car is getting to service");

            InServiceCar inServiceCar = CarsController.GetCarToService(modelName, brandName, serviceReason);

            if (inServiceCar != null)
            {
                ConsoleOutput.InfoMessage($"The {inServiceCar.Brand} {inServiceCar.Model} is now in service now.\nThe reason is \"{inServiceCar.ServiceReason}\"");
            }
            else
            {
                ConsoleOutput.ErrorMessage($"Sorry, there is no such car - {brandName} {modelName}");
            }
        }

        private void CarsTable(List<InServiceCar> cars, string tableName)
        {
            Console.WriteLine(tableName);
            Table table = new Table("#", "Brand", "Model", "Car Price ($)", "Service Reason") { Config = TableConfiguration.MySql() };

            for (int i = 0; i < cars.Count; i++)
            {
                var (model, brand, _, _, _, price, _, serviceReason) = cars[i].GetInServiceCarValues();

                table.AddRow(i + 1, model, brand, price,serviceReason);
            }

            Console.Write(table.ToString());
        }

        private void ShowProfit()
        {
            CommissionController commissionController = new CommissionController(Login);

            ConsoleOutput.InfoMessage($"Your commission percents for work is {commissionController.UserCommission.CommissionPercents}%.\nYour profit is {commissionController.UserCommission.Profit}$");
        }
    }
}
