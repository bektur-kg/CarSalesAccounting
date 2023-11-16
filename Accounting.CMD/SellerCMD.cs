using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using BetterConsoleTables;
using System;
using System.Collections.Generic;

namespace Accounting.CMD
{
    public class SellerCMD
    {
        private const string COMMANDS_LIST = 
        "\n1 - Show all list of cars\n2 - Search a car\n3 - Report of cars\n4 - Order a car\n5 - Show list of sold cars\n6 - Return a car\n7 - Book a car\n8 - Show list of booked cars\nQ - Exit\n";

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
                        CarsTable(CarsController.SoldCars, "\t\t\tList of Sold Cars");

                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D7:
                        BookCar();

                        break;
                    case ConsoleKey.D8:
                        CarsTable(CarsController.BookedCars, "\t\t\tList of Booked Cars");

                        break;
                    case ConsoleKey.Q:
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

        private void CarsTable(List<BookedCar> cars, string tableName)
        {
            Console.WriteLine(tableName);
            Table table = new Table("#", "Brand", "Model", "Body Type", "ATT", "Price ($)", "Fuel Type", "Description", "Start Date", "End Date") { Config = TableConfiguration.MySql() };

            for (int i = 0; i < cars.Count; i++)
            {
                var (model, brand, bodyType, ATT, fuelType, price, description, startDate, endDate) = cars[i].GetBookedCarValues();

                table.AddRow(i + 1, model, brand, bodyType, ATT, price, fuelType, description, startDate, endDate);
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
                ConsoleOutput.ErrorMessage($"Sorry, there is no such car - (Brand: {brand}, Model: {model}) in cars list");
            }
        }

        private void BookCar()
        {
            Console.WriteLine("Booking a car");

            string brand = ConsoleInput.TextType("Brand: ");
            string model = ConsoleInput.TextType("Model: ");
            DateTime startDateTime = ConsoleInput.DateType("Enter Starting Date For Booking: ");
            DateTime endDateTime = ConsoleInput.DateType("Enter Ending Date For Booking: ");

            Car bookedCar = CarsController.BookCar(brand, model, startDateTime, endDateTime);

            if (bookedCar != null)
            {
                ConsoleOutput.InfoMessage($"Brand: {brand}, Model: {model} is booked successfully. Now it is in booked-cars list");
            }
            else
            {
                ConsoleOutput.ErrorMessage($"Sorry, there is no such car - (Brand: {brand}, Model: {model}) in cars list");
            }
        }
    }
}
