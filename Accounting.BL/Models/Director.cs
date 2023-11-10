using System;

namespace Accounting.BL.Models
{
    [Serializable]
    public class Director : User
    {
        public string Name { get; set; }

        public Director(string login)
        {

        }
    }
}
