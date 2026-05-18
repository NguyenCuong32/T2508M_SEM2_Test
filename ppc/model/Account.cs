using System;
using System.Globalization;
using ppc.Interface;

namespace ppc.Model
{
    public abstract class Account : IAccount
    {
        public decimal Balance { get; protected set; }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {FormatCurrency(Balance)}");
        }

        public virtual void Transfer(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Transfer amount must be greater than zero.");
                return;
            }
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"Your transferred {FormatCurrency(amount)}, Your balancer : {FormatCurrency(Balance)}");
        }

        protected string FormatCurrency(decimal amount)
        {
            return amount.ToString("N0", new CultureInfo("vi-VN")).Replace(",", ".") + " đ";
        }
    }
}
