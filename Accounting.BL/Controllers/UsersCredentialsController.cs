using Accounting.BL.Helpers;
using Accounting.BL.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Accounting.BL.Controllers
{
    public class UsersCredentialsController : BaseController
    {
        public List<UsersCredentials> Credentials { get; private set; }
        public bool IsAccountTaken { get; set; }

        public UsersCredentialsController()
        {
            Credentials = GetUsersCredentials();
        }

        public List<UsersCredentials> GetUsersCredentials() 
        {
            return Get<List<UsersCredentials>>(FileNames.USERS_CREDENTIALS) ?? new List<UsersCredentials>();
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
        public bool AddUserCredentials(string login, string password, AccountTypesEnum accountType)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);

            //todo: add chekcer for account type

            UsersCredentials newUserCredentials = new UsersCredentials { Login = login, AccountType = accountType, Password = password };

            if (Credentials.Find(user => user.Login == newUserCredentials.Login) != null)
            {
                return false;
            }
            
            Credentials.Add(newUserCredentials);

            SaveUsersCredentials();

            return true;
        }

        public bool RemoveUserCredentials(string login)
        {
            ArgumentChecker.ArgumentNullChecker(login);

            UsersCredentials foundUserCredentials = Credentials.Find(user => user.Login == login);

            //todo: add checker to prevent deleting last director
            if (foundUserCredentials != null)
            {
                Credentials.Remove(foundUserCredentials);

                SaveUsersCredentials();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CanSignIn(string login, string password, AccountTypesEnum accountType)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);

            //todo: add account type checker

            UsersCredentials userCredentials = Credentials.Find(user => user.Login == login);

            if (userCredentials != null)
            {
                return userCredentials.Password == password && userCredentials.AccountType == accountType;
            }
            else
            {
                return false;
            }
        }
    }
}
