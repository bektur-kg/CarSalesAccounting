using Accounting.BL.Helpers;
using Accounting.BL.Models;
using System;
using System.Collections.Generic;

namespace Accounting.BL.Controllers
{
    public class UsersCredentialsController : BaseController
    {
        public UsersCredentials Credentials { get; private set; }
        public bool IsAccountTaken { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>If that login is free</returns>
        public bool AddUserCredentials(string login, string password)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);

            if (Credentials.UserCredentials.ContainsKey(login))
            {
                return false;
            }
            
            Credentials.UserCredentials.Add(login, password);

            SaveUsersCredentials();

            return true;
        }

        public void RemoveUserCredentials(string login)
        {
            ArgumentChecker.ArgumentNullChecker(login);

            Credentials.UserCredentials.Remove(login);

            SaveUsersCredentials();
        }

        public bool CanSignIn(string login, string password)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);

            if (Credentials.UserCredentials.ContainsKey(login))
            {
                return Credentials.UserCredentials[login] == password;
            }
            else
            {
                return false;
            }
        }
    }
}
