using Accounting.BL.Models.AutoMobile;
using System;

namespace Accounting.BL.Models.Automobile
{
    [Serializable]
    public class InServiceCar : Car
    {
        public string ServiceReason { get; private set; }

        public InServiceCar(string model, string brand, CarBodyTypesEnum bodyType, ATTEnum ATT, double price, string description, FuelTypeEnum fuelType, string serviceReason) : base(model, brand, bodyType, ATT, price, description, fuelType)
        {
            ServiceReason = serviceReason;
        }

        public (string model, string brand, CarBodyTypesEnum bodyType, ATTEnum ATT, FuelTypeEnum fuelType, double price, string description, string serviceReason) GetInServiceCarValues()
        {
            var (model, brand, bodyType, ATT, fuelType, price, description) = GetCarValues();
            return (model, brand, bodyType, ATT, fuelType, price, description, ServiceReason);
        }
    }
}
