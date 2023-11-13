using System;
using System.Collections.Generic;

namespace Accounting.BL.Models
{
    [Serializable]
    public class UsersCredentials
    {
        public Dictionary<string, string> UserCredentials { get; set; }


        public UsersCredentials()
        {
            UserCredentials = new Dictionary<string, string>();
        }
    }
}
