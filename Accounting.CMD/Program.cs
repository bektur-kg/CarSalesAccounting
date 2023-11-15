using System;
using Accounting.BL.Controllers;
using Accounting.BL.Helpers;
using Accounting.BL.Models;

namespace Accounting.CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\tCar Sales Accounting");
            Console.ForegroundColor = ConsoleColor.Green;

            AccountTypesEnum accountType = EnterAccountType();
            string login = EnterLoginAndPassword(accountType);

            switch (accountType)
            {
                case AccountTypesEnum.Repairman:
                    RepairmanCMD repairmanCMD = new RepairmanCMD();

                    repairmanCMD.CommandsList();
                    break;
                case AccountTypesEnum.Seller:
                    SellerCMD sellerCMD = new SellerCMD(login);

                    sellerCMD.CommandsList();
                    break;
                case AccountTypesEnum.Director:
                    DirectorCMD directorCMD = new DirectorCMD(login);

                    directorCMD.CommandsList();
                    break;
            }
        }

        public static AccountTypesEnum EnterAccountType()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nEnter your account type please (Seller, Repairman, Director)");
                string accountType = Console.ReadLine();

                if (accountType == AccountTypesEnum.Seller.ToString() ||
                    accountType == AccountTypesEnum.Director.ToString() ||
                    accountType == AccountTypesEnum.Repairman.ToString())
                {
                    if (Enum.TryParse(accountType, out AccountTypesEnum AccountType))
                    {
                        return AccountType;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Account Type. Please, try again!");
                    continue;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>login of an account</returns>
        public static string EnterLoginAndPassword(AccountTypesEnum accountType)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                string login = ConsoleInput.TextType("Enter you login: ");
                string password = ConsoleInput.TextType("Enter your password: ");

                UsersCredentialsController userCredentialsController = new UsersCredentialsController();

                if (userCredentialsController.CanSignIn(login, password, accountType))
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome to Car Sales Accounting, {login}\n");

                    return login;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry, but the login or password is incorrect");
                    continue;
                }
            }
        }
    }
}
