using Accounting.BL.Helpers;
using System;

namespace Accounting.BL.Models.Account
{
    [Serializable]
    public class Commission
    {
        public double CommissionPercents { get; set; }
        public User User { get; set; }
        public double Profit { get;  set; }

        public Commission(User user, double commissionPercents)
        {
            ArgumentChecker.ArgumentNullChecker(user);
            ArgumentChecker.CheckPrice(commissionPercents);

            CommissionPercents = commissionPercents;
            User = user;
        }

    }
}
