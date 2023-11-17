using System;

namespace Accounting.BL.Models
{
    [Serializable]
    public class User
    {
        public string Login { get; private set; }
        public AccountTypesEnum AccountType { get; private set; }

        public User(string login, AccountTypesEnum accountType)
        {
            Login = login;
            AccountType = accountType;  
        }
    }
}
