﻿using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models.AutoMobile;
using BetterConsoleTables;
using System;
using System.Collections.Generic;

namespace Accounting.CMD
{
    public class SellerCMD
    {
        private const string COMMANDS_LIST = 
        "\n1 - Show all list of cars\n2 - Search a car\n3 - Report of cars\n4 - Order a car\n5 - Show list of sold cars\n6 - Return a car\n7 - Book a car\nQ - Exit\n";

        public CarsController CarsController { get; private set; }

        public SellerCMD(string login)
        {
            CarsController = new CarsController(login);
        }

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
                        CarsTable(CarsController.Cars, "\t\t\tCars Table");

                        break;
                    case ConsoleKey.D2:

                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        OrderACar();

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

        private void CarsTable(List<Car> cars, string tableName)
        {
            Console.WriteLine(tableName);
            Table table = new Table("#", "Brand", "Model", "Body Type", "ATT", "Price ($)", "Fuel Type", "Description") { Config = TableConfiguration.MySql() };

            for (int i = 0; i < cars.Count; i++)
            {
                var (model, brand, bodyType, ATT, fuelType, price, description) = cars[i].GetCarValues();

                table.AddRow(i + 1, model, brand, bodyType, ATT, price, fuelType, description);
            }

            Console.Write(table.ToString());
        }

        private void OrderACar()
        {
            Console.WriteLine("Ordering a car");

            string brand = ConsoleInput.TextType("Brand: ");
            string model = ConsoleInput.TextType("Model: ");

            Car orderedCar = CarsController.OrderACar(brand, model);

            if (orderedCar != null)
            {
                Console.WriteLine($"Brand: {brand}, Model: {model} is ordered successfully. Now it is in sold-cars list");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, there is no such car in cars list");
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
    }
}
