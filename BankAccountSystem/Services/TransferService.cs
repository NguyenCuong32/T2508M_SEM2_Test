using BankAccountSystem.Interfaces;

namespace BankAccountSystem.Services
{
    class TransferService
    {
        public void Transfer(ITransfer account, decimal money)
        {
            if (money > account.Balance)
            {
                Console.WriteLine("Not enough balance!");
                return;
            }

            account.Balance -= money;

            Console.WriteLine($"\nYour transferred: {money:N0} đ");
            Console.WriteLine($"Your balance: {account.Balance:N0} đ");
        }
    }
}