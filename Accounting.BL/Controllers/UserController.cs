using Accounting.BL.Models;
using System.Collections.Generic;

namespace Accounting.BL.Controllers
{
    public class UserController : BaseController
    {
        AccountTypesEnum AccountType { get; set; }
        public Dictionary<AccountTypesEnum, List<User>> Accounts { get; private set; }


        public UserController(AccountTypesEnum accountType)
        {
            AccountType = accountType;
            Accounts = GetAllAccounts();
        }

        public Dictionary<AccountTypesEnum, List<User>> GetAllAccounts()
        {
            return Get<Dictionary<AccountTypesEnum, List<User>>>(FileNames.ACCOUNTS) ?? new Dictionary<AccountTypesEnum, List<User>>();
        }
    }
}
