using System;

namespace Accounting.BL.Models.Account
{
    [Serializable]
    public class Commission
    {
        public double CommissionPercents { get; set; }
        public User User { get; set; }

        public Commission(User user, double commissionPercents)
        {
            //todo: add user and percents checker
            CommissionPercents = commissionPercents;
            User = user;
        }
    }
}
