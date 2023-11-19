using Accounting.BL.Helpers;
using Accounting.BL.Models;
using Accounting.BL.Models.Account;
using System.Collections.Generic;
using System.Linq;

namespace Accounting.BL.Controllers
{
    public class CommissionController : BaseController
    {
        public List<Commission> CommissionsList { get; private set; }
        public Commission UserCommission { get; private set; }

        public CommissionController(string login)
        {
            ArgumentChecker.ArgumentNullChecker(login);

            CommissionsList = Get<List<Commission>>(FileNames.COMMISSIONS) ?? new List<Commission>();
            UserCommission = CommissionsList.FirstOrDefault(commission => commission.User.Login == login);
        }

        public void AddUserCommission(User user, double commissionPercents)
        {
            ArgumentChecker.ArgumentNullChecker(user);
            ArgumentChecker.CheckPrice(commissionPercents);

            Commission commission = new Commission(user, commissionPercents / 100);
            CommissionsList.Add(commission);
            SaveCommissionsList();
        }

        private void SaveCommissionsList()
        {
            Post(FileNames.COMMISSIONS, CommissionsList);
        }

        public double TakeProfit(double price)
        {
            ArgumentChecker.CheckPrice(price);

            double revenue = price * (UserCommission.CommissionPercents / 100);
            UserCommission.Profit += revenue;

            SaveCommissionsList();
            return revenue;
        }
    }
}
