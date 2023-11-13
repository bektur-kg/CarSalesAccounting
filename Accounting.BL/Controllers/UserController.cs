using Accounting.BL.Helpers;
using Accounting.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Accounting.BL.Controllers
{
    public class UserController : BaseController
    {
        public User Account { get; private set; }
        public List<User> AllAccounts { get; private set; }


        public UserController(string login)
        {
            AllAccounts = GetAllAccounts();
            Account = AllAccounts.FirstOrDefault(account => account.Login == login);
        }

        public List<User> GetAllAccounts()
        {
            return Get<List<User>>(FileNames.ACCOUNTS) ?? new List<User>();
        }

        public void SaveAllAccounts()
        {
            Post(FileNames.ACCOUNTS, AllAccounts);
        }

        public void CreateNewUser(string login, string password, AccountTypesEnum newUserAccountType)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);

            //todo: add newUserAccountType checker

            AccountTypeChecker.IsDirectorAccount(Account.AccountType);

            UsersCredentialsController usersCredentialsController = new UsersCredentialsController();
            usersCredentialsController.AddUserCredentials(login, password, newUserAccountType);

            User newUser = new User(login, newUserAccountType);
            AllAccounts.Add(newUser);

            SaveAllAccounts();
        }
    }
}
