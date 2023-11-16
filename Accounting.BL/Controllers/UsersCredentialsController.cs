using Accounting.BL.Exceptions;
using Accounting.BL.Helpers;
using Accounting.BL.Models;
using System.Collections.Generic;

namespace Accounting.BL.Controllers
{
    public class UsersCredentialsController : BaseController
    {
        public List<UsersCredentials> Credentials { get; private set; }

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

        public void AddUserCredentials(string login, string password, AccountTypesEnum accountType)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);
            ArgumentChecker.CheckIfEnumIsAssigned(accountType);

            UsersCredentials newUserCredentials = new UsersCredentials { Login = login, AccountType = accountType, Password = password };

            if (Credentials.Find(user => user.Login == newUserCredentials.Login) != null)
            {
                throw new AccountIsTakenException($"{login} is taken");
            }
            
            Credentials.Add(newUserCredentials);

            SaveUsersCredentials();
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
            ArgumentChecker.CheckIfEnumIsAssigned(accountType);

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
