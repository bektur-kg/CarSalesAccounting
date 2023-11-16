using Accounting.BL.Models.AutoMobile;
using System;

namespace Accounting.BL.Models.Automobile
{
    public class BookedCar : Car
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }


        public BookedCar(string model, string brand, CarBodyTypesEnum bodyType, ATTEnum ATT, double price, string description, FuelTypeEnum fuelType, DateTime startDate, DateTime endDate)
            : base(model, brand, bodyType, ATT, price, description, fuelType)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public (string model, string brand, CarBodyTypesEnum bodyType, ATTEnum ATT, FuelTypeEnum fuelType, double price, string description, DateTime startDate, DateTime endDate) GetBookedCarValues()
        {
            var (model, brand, bodyType, ATT, fuelType, price, description) = GetCarValues();

            return (model, brand, bodyType, ATT, fuelType, price, description, StartDate, EndDate);
        }
    }
}
