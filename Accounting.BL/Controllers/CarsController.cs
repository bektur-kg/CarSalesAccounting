using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Accounting.BL.Controllers
{
    public class CarsController : UserController
    {
        public List<Car> Cars { get; private set; }
        public List<Car> SoldCars { get; private set; }
        public List<BookedCar> BookedCars { get; private set; }

        public CarsController(string login) : base(login)
        {
            SoldCars = Get<List<Car>>(FileNames.SOLD_CARS) ?? new List<Car>();
            Cars = Get<List<Car>>(FileNames.CARS) ?? new List<Car>();
            BookedCars = Get<List<BookedCar>>(FileNames.BOOKED_CARS) ?? new List<BookedCar>();
        }

        private void SaveCars()
        {
            Post(FileNames.CARS, Cars);
            Post(FileNames.SOLD_CARS, SoldCars);
            Post(FileNames.BOOKED_CARS, BookedCars);
        }

        public void AddCar(string model, string brand, CarBodyTypesEnum bodyType, ATTEnum ATT, double price, string description, FuelTypeEnum fuelType)
        {
            AccountTypeChecker.IsDirectorAccount(Account.AccountType);

            Car newCar = new Car(model, brand, bodyType, ATT, price, description, fuelType);
            Cars.Add(newCar);

            SaveCars();
        }

        public Car GetPriciestCar() => Cars.OrderByDescending(car => car.Price).FirstOrDefault();

        public Car GetCheapestCar() => Cars.OrderBy(car => car.Price).FirstOrDefault();

        public Car OrderACar(string brand, string model)
        {
            Car carToOrder = Cars.FirstOrDefault(car => car.Brand == brand && car.Model == model);

            if (carToOrder != null)
            {
                Cars.Remove(carToOrder);
                SoldCars.Add(carToOrder);
                SaveCars();
            }

            return carToOrder;
        }

        public Car BookCar(string bookingCarBrand, string bookingCarModel, DateTime startDate, DateTime endDate)
        {
            Car foundCar = Cars.FirstOrDefault(car => car.Brand == bookingCarBrand && car.Model == bookingCarModel);
            if (foundCar != null)
            {
                var (model, brand, bodyType, ATT, fuelType, price, description) = foundCar.GetCarValues();
                BookedCar carToBook = new BookedCar(model, brand, bodyType, ATT, price, description, fuelType, startDate, endDate);

                Cars.Remove(foundCar);
                BookedCars.Add(carToBook);
                SaveCars();
            }

            return foundCar;
        }
    }
}
