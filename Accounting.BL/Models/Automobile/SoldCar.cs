using Accounting.BL.Helpers;
using Accounting.BL.Models.AutoMobile;
using System;

namespace Accounting.BL.Models.Automobile
{
    [Serializable]
    public class SoldCar
    {
        public Car Car { get; private set; }
        public double TaxPercents { get; private set; }
        public double TotalPrice { get; private set; }
        public double TaxPrice { get => Car.Price * TaxPercents; }
        public double CommissionPrice { get; private set; }

        public SoldCar(Car car, double taxPercents, double totalPrice, double commissionPrice)
        {
            ArgumentChecker.ArgumentNullChecker(car);
            ArgumentChecker.CheckPrice(taxPercents, totalPrice);

            Car = car;
            TaxPercents = taxPercents;
            TotalPrice = totalPrice;
            CommissionPrice = commissionPrice;
        }
    }
}
