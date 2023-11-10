using System;
using System.Security.AccessControl;
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
            EnterLoginAndPassword();

            switch (accountType)
            {
                case AccountTypesEnum.Repairman:

                    break;
                case AccountTypesEnum.Seller:
                    break;
                case AccountTypesEnum.Director:
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

        public static void EnterLoginAndPassword()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("Enter you login: ");
                string login = Console.ReadLine();

                Console.Write("Enter your password: ");
                string password = Console.ReadLine();

                UsersCredentialsController userCredentialsController = new UsersCredentialsController();

                if (userCredentialsController.CanSignIn(login, password))
                {
                    Console.WriteLine($"Welcome to Car Sales Accounting, {login}");
                    break;
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
