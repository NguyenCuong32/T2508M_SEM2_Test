using BankAccountSystem.Models;
using BankAccountSystem.Services;

namespace BankAccountSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            TransferService transferService = new TransferService();

            Console.WriteLine("===== BANK ACCOUNT SYSTEM =====");
            Console.WriteLine("1. Normal Account");
            Console.WriteLine("2. Exchange Account");

            Console.Write("\nChoose account type: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateNormalAccount(transferService);
                    break;

                case 2:
                    CreateExchangeAccount(transferService);
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            Console.ReadKey();
        }

        static void CreateNormalAccount(TransferService transferService)
        {
            Console.Write("\nEnter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            NormalAccount account = new NormalAccount(amount);

            account.CheckBalance();

            Console.Write("\nEnter transfer amount: ");
            decimal transferMoney = decimal.Parse(Console.ReadLine());

            transferService.Transfer(account, transferMoney);
        }

        static void CreateExchangeAccount(TransferService transferService)
        {
            Console.Write("\nEnter USD amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter exchange rate: ");
            decimal rate = decimal.Parse(Console.ReadLine());

            ExchangeAccount account = new ExchangeAccount(amount, rate);

            account.CheckBalance();

            Console.Write("\nEnter transfer amount: ");
            decimal transferMoney = decimal.Parse(Console.ReadLine());

            transferService.Transfer(account, transferMoney);
        }
    }
}