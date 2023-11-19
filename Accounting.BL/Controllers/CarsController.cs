using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounting.BL.Controllers
{
    public class CarsController : UserController
    {
        public List<Car> Cars { get; private set; }
        public List<SoldCar> SoldCars { get; private set; }
        public List<BookedCar> BookedCars { get; private set; }
        public List<InServiceCar> InServiceCars { get; private set; }
        public List<InServiceCar> ServedCars { get; private set; }

        public CarsController(string login) : base(login)
        {
            SoldCars = Get<List<SoldCar>>(FileNames.SOLD_CARS) ?? new List<SoldCar>();
            Cars = Get<List<Car>>(FileNames.CARS) ?? new List<Car>();
            BookedCars = Get<List<BookedCar>>(FileNames.BOOKED_CARS) ?? new List<BookedCar>();
            InServiceCars = Get<List<InServiceCar>>(FileNames.IN_SERVICE_CARS) ?? new List<InServiceCar>();
            ServedCars = Get<List<InServiceCar>>(FileNames.SERVED_CARS) ?? new List<InServiceCar>();

            UpdateBookedCarsStatus();
        }

        private void SaveCars()
        {
            Post(FileNames.CARS, Cars);
            Post(FileNames.SOLD_CARS, SoldCars);
            Post(FileNames.BOOKED_CARS, BookedCars);
            Post(FileNames.IN_SERVICE_CARS, InServiceCars);
            Post(FileNames.SERVED_CARS, ServedCars);
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

        public (SoldCar soldCar, double userCommissionPercents, double userCommissionPrice, double totalPrice) OrderCar(string brand, string model, double taxPercents)
        {
            Car carToOrder = Cars.FirstOrDefault(car => car.Brand == brand && car.Model == model);

            if (carToOrder != null)
            {
                CommissionController userCommissionController = new CommissionController(Account.Login);
                double userCommissionPrice = userCommissionController.TakeProfit(carToOrder.Price);

                double totalPrice = userCommissionPrice + carToOrder.Price + carToOrder.Price * (taxPercents / 100);

                SoldCar soldCar = new SoldCar(carToOrder, taxPercents / 100, totalPrice, userCommissionPrice);

                Cars.Remove(carToOrder);
                SoldCars.Add(soldCar);
                SaveCars();

                return (soldCar, userCommissionController.UserCommission.CommissionPercents, userCommissionPrice, soldCar.TotalPrice);
            }

            return default;
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

        public void UpdateBookedCarsStatus()
        {
            foreach (BookedCar car in BookedCars)
            {   
                if (car.EndDate < DateTime.Now)
                {
                    BookedCars.Remove(car);
                    Cars.Add(car);
                    return;
                }
            }
        }

        public SoldCar ReturnSoldCar(string returningCarBrand, string returningCarModel)
        {
            SoldCar carToReturn = SoldCars.FirstOrDefault(car => car.Car.Brand == returningCarBrand && car.Car.Model == returningCarModel);

            if (carToReturn != null)
            {
                SoldCars.Remove(carToReturn);
                Cars.Add(carToReturn.Car);
                SaveCars();
            }

            return carToReturn;
        }

        public List<Car> SearchCar(SearchTypesEnum searchType, string searchValue)
        {
            return Cars.Where(car => car.GetType()
                            .GetProperty(searchType.ToString())
                            .GetValue(car).ToString() == searchValue).ToList();
        }

        public Car GetCarWithMaxSells()
        {
            return SoldCars
                .GroupBy(car => car)
                .OrderBy(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault().Car;
        }

        public Car GetCarWithMinSells()
        {
            return SoldCars
                .GroupBy(car => car)
                .OrderBy(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault().Car;
        }

        public InServiceCar GetCarToService(string carToServiceModel, string carToServiceBrand, string serviceReason)
        {
            Car carToGetToService = Cars.FirstOrDefault(car => car.Model == carToServiceModel && car.Brand == carToServiceBrand);
            var (model, brand, bodyType, ATT, fuelType, price, description) = carToGetToService.GetCarValues();

            if (carToGetToService != null)
            {
                InServiceCar newInServiceCar = new InServiceCar(model, brand, bodyType, ATT, price, description, fuelType, serviceReason);

                InServiceCars.Add(newInServiceCar);
                Cars.Remove(carToGetToService);
                SaveCars();

                return newInServiceCar;
            }

            return default;
        }

        public (Car car, double userCommissionPercents, double userCommissionPrice) FinishCarService(string ServingCarBrand, string ServingCarModel, double workPrice)
        {
            InServiceCar finishingServiceCar = InServiceCars.FirstOrDefault(car => car.Brand == ServingCarBrand && car.Model == ServingCarModel);

            if (finishingServiceCar != null)
            {
                CommissionController userCommissionController = new CommissionController(Account.Login);
                double userCommissionPrice = userCommissionController.TakeProfit(workPrice);
                var (model, brand, bodyType, ATT, fuelType, price, description, _) = finishingServiceCar.GetInServiceCarValues();

                Car servedCar = new Car(model, brand, bodyType, ATT, price, description, fuelType);

                InServiceCars.Remove(finishingServiceCar);
                ServedCars.Add(finishingServiceCar);
                Cars.Add(servedCar);
                SaveCars();

                return (servedCar, userCommissionController.UserCommission.CommissionPercents, userCommissionPrice);
            }

            return default;
        }
    }
}
