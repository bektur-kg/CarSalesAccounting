using Accounting.BL.Exceptions;
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

        public void CreateNewUser(string login, string password, AccountTypesEnum newUserAccountType, double userCommissionPercents)
        {
            ArgumentChecker.ArgumentNullChecker(login, password);
            ArgumentChecker.CheckIfEnumIsAssigned(newUserAccountType);

            AccountTypeChecker.IsDirectorAccount(Account.AccountType);

            if (newUserAccountType == AccountTypesEnum.Director)
            {
                throw new CreatingDirectorException("Creating a new Director account, but there is one exists");
            }

            UsersCredentialsController usersCredentialsController = new UsersCredentialsController();
            usersCredentialsController.AddUserCredentials(login, password, newUserAccountType);

            User newUser = new User(login, newUserAccountType);
            AllAccounts.Add(newUser);

            if (newUserAccountType == AccountTypesEnum.Seller || newUserAccountType == AccountTypesEnum.Repairman)
            {
                CommissionController commissionController = new CommissionController(newUser.Login);
                commissionController.AddUserCommission(newUser, userCommissionPercents);
            }

            SaveAllAccounts();
        }

        public User GetAccountData(string login, AccountTypesEnum accountType) => AllAccounts.FirstOrDefault(account => account.Login == login && account.AccountType == accountType);
    
        public User RemoveUser(string login)
        {
            ArgumentChecker.ArgumentNullChecker(login);
            AccountTypeChecker.IsDirectorAccount(Account.AccountType);

            User userToRemove = AllAccounts.FirstOrDefault(account => account.Login == login);

            if (userToRemove?.AccountType == AccountTypesEnum.Director)
            {
                throw new DeletingDirectorException();
            }

            UsersCredentialsController usersCredentialsController = new UsersCredentialsController();

            if (userToRemove != null)
            {
                AllAccounts.Remove(userToRemove);
                SaveAllAccounts();
                usersCredentialsController.RemoveUserCredentials(userToRemove.Login);
            }

            return userToRemove;
        }

        public (string login, AccountTypesEnum accountType, string password, double profit, double commissionPercent) GetAllAccountData(string login)
        {
            UsersCredentialsController userCredentialsController = new UsersCredentialsController();
            UsersCredentials userCredentials = userCredentialsController.Credentials.FirstOrDefault(credential => credential.Login == login);

            CommissionController commissionController = new CommissionController(login);

            return (userCredentials.Login, userCredentials.AccountType, userCredentials.Password, commissionController.UserCommission.Profit, commissionController.UserCommission.CommissionPercents);
        }
    }
}
