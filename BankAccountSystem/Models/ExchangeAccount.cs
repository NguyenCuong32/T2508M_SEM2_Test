using BankAccountSystem.Interfaces;

namespace BankAccountSystem.Models
{
    class ExchangeAccount : Account, IExchange
    {
        public decimal ExchangeRate { get; set; }

        public ExchangeAccount(decimal amount, decimal exchangeRate)
        {
            Amount = amount;
            ExchangeRate = exchangeRate;

            Exchange();
        }

        public void Exchange()
        {
            Balance = Amount * ExchangeRate;
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"\nYour balance: {Balance:N0} đ");
        }
    }
}