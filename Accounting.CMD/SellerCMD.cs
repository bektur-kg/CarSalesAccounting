using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models;
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
        "\n1 - Show all list of cars\n2 - Search a car\n3 - Report of cars\n4 - Order a car\n5 - Show list of sold cars\n6 - Return a car\n7 - Book a car\n8 - Show list of booked cars\n9 - Show my profit\nQ - Exit\n";

        public CarsController CarsController { get; private set; }
        public string Login { get; private set; }

        public SellerCMD(string login)
        {
            CarsController = new CarsController(login);
            Login = login;
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
                        SearchCars();    

                        break;
                    case ConsoleKey.D3:
                        CarsReport();

                        break;
                    case ConsoleKey.D4:
                        OrderACar();

                        break;
                    case ConsoleKey.D5:
                        CarsTable(CarsController.SoldCars, "\t\t\tList of Sold Cars");
                        
                        break;
                    case ConsoleKey.D6:
                        ReturnSoldCar();

                        break;
                    case ConsoleKey.D7:
                        BookCar();

                        break;
                    case ConsoleKey.D8:
                        CarsTable(CarsController.BookedCars, "\t\t\tList of Booked Cars");

                        break;
                    case ConsoleKey.D9:
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

        private void ShowProfit()
        {
            CommissionController commissionController = new CommissionController(Login);

            ConsoleOutput.InfoMessage($"Your commission percents for work is {commissionController.UserCommission.CommissionPercents}%.\nYour profit is {commissionController.UserCommission.Profit}$");
        }

        private void CarsReport()
        {
            CarsTable(CarsController.Cars, "\t\t\tAll Cars available to sell");
            CarsTable(CarsController.SoldCars, "\t\t\tAll Sold Cars");
            CarsTable(CarsController.InServiceCars, "\t\t\tAll in-service cars");
            CarsTable(CarsController.BookedCars, "\t\t\tAll booked cars");
        }

        private void SearchCars()
        {
            SearchTypesEnum searchType = ConsoleInput.EnumType<SearchTypesEnum>("Search by (Model, Brand, BodyType, ATT): ");
            string searchValue = ConsoleInput.TextType($"Searching by {searchType}: ");

            List<Car> foundCars =  CarsController.SearchCar(searchType, searchValue);
            CarsTable(foundCars, "Found Cars by Your Search");
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

        private void CarsTable(List<SoldCar> cars, string tableName)
        {
            Console.WriteLine(tableName);
            Table table = new Table("#", "Brand", "Model", "Car Price ($)", "Tax Price", "Commission Price", "Total Price") { Config = TableConfiguration.MySql() };

            for (int i = 0; i < cars.Count; i++)
            {
                var (model, brand, _, _, _, price, _) = cars[i].Car.GetCarValues();
                double taxPrice = cars[i].TaxPrice;
                double totalPrice = cars[i].TotalPrice;
                double commissionPrice = cars[i].CommissionPrice;

                table.AddRow(i + 1, model, brand, price, taxPrice, commissionPrice, totalPrice);
            }

            Console.Write(table.ToString());
        }

        private void CarsTable(List<BookedCar> cars, string tableName)
        {
            Console.WriteLine(tableName);
            Table table = new Table("#", "Brand", "Model", "Body Type", "ATT", "Price ($)", "Fuel Type", "Description", "Start Date", "End Date") { Config = TableConfiguration.MySql() };

            CarsController.UpdateBookedCarsStatus();
            for (int i = 0; i < cars.Count; i++)
            {
                var (model, brand, bodyType, ATT, fuelType, price, description, startDate, endDate) = cars[i].GetBookedCarValues();

                table.AddRow(i + 1, brand, model, bodyType, ATT, price, fuelType, description, startDate, endDate);
            }

            Console.Write(table.ToString());
        }

        private void CarsTable(List<InServiceCar> cars, string tableName)
        {
            Console.WriteLine(tableName);
            Table table = new Table("#", "Brand", "Model", "Car Price ($)", "Service Reason") { Config = TableConfiguration.MySql() };

            for (int i = 0; i < cars.Count; i++)
            {
                var (model, brand, _, _, _, price, _, serviceReason) = cars[i].GetInServiceCarValues();

                table.AddRow(i + 1, model, brand, price, serviceReason);
            }

            Console.Write(table.ToString());
        }

        private void OrderACar()
        {
            Console.WriteLine("Ordering a car");

            string brand = ConsoleInput.TextType("Brand: ");
            string model = ConsoleInput.TextType("Model: ");
            double taxPercents = ConsoleInput.PriceType("Tax Percents: ");

            var (orderedCar, userCommissionPercents, userCommissionPrice, totalPrice) = CarsController.OrderCar(brand, model, taxPercents);

            if (orderedCar != null)
            {
                Console.WriteLine($"The Price for {orderedCar.Car.Brand} {orderedCar.Car.Model} is {orderedCar.Car.Price}$.\nTax price is {orderedCar.TaxPercents}% or {orderedCar.TaxPrice}$.\n" +
                    $"Commission price for seller is {userCommissionPercents}% or {userCommissionPrice}$.\n\nThe Total Price is - {totalPrice}$");
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

        private void ReturnSoldCar()
        {
            Console.WriteLine("Returning Sold Car");
            string brand = ConsoleInput.TextType("Brand: ");
            string model = ConsoleInput.TextType("Model: ");

            SoldCar returnedCar = CarsController.ReturnSoldCar(brand, model);

            if (returnedCar != null)
            {
                ConsoleOutput.InfoMessage($"Brand: {brand}, Model: {model} is returned successfully. Now it is in cars list");
            }
            else
            {
                ConsoleOutput.ErrorMessage($"Sorry, there is no such car - (Brand: {brand}, Model: {model}) in sold-cars list");
            }
        }
    }
}
