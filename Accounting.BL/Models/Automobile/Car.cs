using Accounting.BL.Models.Automobile;
using System;

namespace Accounting.BL.Models.AutoMobile
{
    [Serializable]
    public class Car
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public CarBodyTypesEnum BodyType { get; set; }
        public ATTEnum ATT { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public Car(string model, string brand, CarBodyTypesEnum bodyType, ATTEnum ATT, double price, string description, FuelTypeEnum fuelType)
        {
            BodyType = bodyType;
            Model = model;
            Brand = brand;
            this.ATT = ATT;
            Price = price;
            Description = description;
            FuelType = fuelType;
        }
    }
}
