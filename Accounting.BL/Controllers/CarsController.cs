using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Automobile;
using Accounting.BL.Models.AutoMobile;
using System.Collections.Generic;

namespace Accounting.BL.Controllers
{
    public class CarsController : UserController
    {
        public List<Car> Cars { get; private set; }

        public CarsController(string login) : base(login)
        {
            Cars = GetCars();
        }

        private List<Car> GetCars()
        {
            return Get<List<Car>>(FileNames.CARS) ?? new List<Car>();
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
    }
}
