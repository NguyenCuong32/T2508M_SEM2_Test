using System;

namespace BankSystem
{
    // Interface
    interface IAccount
    {
        void CheckBalance();
        void Transfer(decimal money);
    }

    // Abstract class
    abstract class Account : IAccount
    {
        protected decimal balance;

        public Account(decimal amount)
        {
            balance = amount;
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balance: {balance:N0} đ");
        }

        public virtual void Transfer(decimal money)
        {
            balance -= money;

            Console.WriteLine(
                $"Your transferred {money:N0} đ, Your balance: {balance:N0} đ"
            );
        }
    }

    // Normal Account
    class NormalAccount : Account
    {
        public NormalAccount(decimal amount) : base(amount)
        {
        }
    }

    // Exchange Account
    class ExchangeAccount : Account
    {
        private decimal exchangeRate;

        public ExchangeAccount(decimal usd, decimal rate)
            : base(usd * rate)
        {
            exchangeRate = rate;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter amount USD:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            ExchangeAccount ex = new ExchangeAccount(amount, 25000);

            ex.CheckBalance();

            ex.Transfer(1000000);

            Console.ReadLine();
        }
    }
}