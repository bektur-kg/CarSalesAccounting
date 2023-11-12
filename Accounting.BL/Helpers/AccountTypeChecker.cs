using Accounting.BL.Models;
using System;

namespace Accounting.BL.Helpers
{
    public static class AccountTypeChecker
    {
        public static void IsDirectorAccount(AccountTypesEnum accountType)
        {
            if(accountType != AccountTypesEnum.Director)
            {
                throw new Exception("Your account type isn't director");
            }
        }
    }
}
