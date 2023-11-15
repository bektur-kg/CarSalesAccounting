using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using System.Collections.Generic;
using System.Linq;

namespace Accounting.BL.Controllers
{
    public class CarsController : UserController
    {
        public List<Car> Cars { get; private set; }
        public List<Car> SoldCars { get; private set; }

        public CarsController(string login) : base(login)
        {
            Cars = Get<List<Car>>(FileNames.CARS) ?? new List<Car>();
            SoldCars = Get<List<Car>>(FileNames.SOLD_CARS) ?? new List<Car>();
        }

        private void SaveCars()
        {
            Post(FileNames.CARS, Cars);
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
            }

            return carToOrder;
        }
    }
}
