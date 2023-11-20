using System;   
using System.Collections.Generic;

namespace Accounting.BL.Models
{
    [Serializable]
    public class UsersCredentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountTypesEnum AccountType { get; set; }
    }
}
