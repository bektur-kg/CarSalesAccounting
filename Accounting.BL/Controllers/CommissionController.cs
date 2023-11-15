using Accounting.BL.Models;
using Accounting.BL.Models.Account;
using System.Collections.Generic;

namespace Accounting.BL.Controllers
{
    public class CommissionController : BaseController
    {
        public List<Commission> CommissionsList { get; set; }

        public CommissionController()
        {
            CommissionsList = Get<List<Commission>>(FileNames.COMMISSIONS) ?? new List<Commission>();
        }

        public void AddUserCommission(User user, double commissionPercents)
        {
            //todo: add user and commissions checker
            Commission commission = new Commission(user, commissionPercents);
            CommissionsList.Add(commission);
            SaveCommissionsList();
        }

        private void SaveCommissionsList()
        {
            Post(FileNames.COMMISSIONS, CommissionsList);
        }
    }
}
