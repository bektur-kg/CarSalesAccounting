using Accounting.BL.Models;
using System;
using System.Collections.Generic;

namespace Accounting.BL.Controllers
{
    public class UsersCredentialsController : BaseController
    {
        public UsersCredentials Credentials { get; set; }

        public UsersCredentialsController()
        {
            Credentials = GetUsersCredentials();
        }

        public UsersCredentials  GetUsersCredentials() 
        {
            return Get<UsersCredentials>(FileNames.USERS_CREDENTIALS) ?? new UsersCredentials();
        }

        public void SaveUsersCredentials()
        {
            Post(FileNames.USERS_CREDENTIALS, Credentials);
        }

        public void AddUserCredentials(string login, string password)
        {
            //todo: add null checker for login & password

            if (Credentials.UserCredentials.ContainsKey(login))
            {
                //todo
            }
            
            Credentials.UserCredentials.Add(login, password);

            SaveUsersCredentials();
        }

        public void RemoveUserCredentials()
        {
            //todo
        }
    }
}
