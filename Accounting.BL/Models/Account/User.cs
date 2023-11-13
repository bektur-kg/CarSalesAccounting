using System;

namespace Accounting.BL.Models
{
    [Serializable]
    public class User
    {
        public string Login { get; set; }
        public AccountTypesEnum AccountType { get; set; }

        public User(string login, AccountTypesEnum accountType)
        {
            Login = login;
            AccountType = accountType;
        }
    }
}
