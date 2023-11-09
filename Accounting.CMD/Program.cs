using System;
using Accounting.BL.Controllers;
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
                Console.Write("Enter you login: ");
                string login = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();

               // if ()
                {

                }
            }
        }
    }
}
