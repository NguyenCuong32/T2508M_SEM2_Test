using System;

namespace AssignmentCS
{
    public interface IAccount
    {
        void CheckBalance();
        void BankTransfer(decimal amount);
    }

    public abstract class BankAccount : IAccount
    {
        public decimal Balance { get; set; }

        protected BankAccount(decimal initialBalance)
        {
            Balance = initialBalance;
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {Balance:N0} d");
        }

        public virtual void BankTransfer(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Số dư tài khoản không đủ.");
                return;
            }
            Balance -= amount;
            Console.WriteLine($"Your transferred {amount:N0} d, Your balancer : {Balance:N0} d");
        }
    }

    public class NormalAccount : BankAccount
    {
        public NormalAccount(decimal initialBalance) : base(initialBalance) { }
    }

    public class ExchangeAccount : BankAccount
    {
        public decimal ExchangeRate { get; set; }

        public ExchangeAccount(decimal initialAmount, decimal exchangeRate) 
            : base(initialAmount * exchangeRate)
        {
            ExchangeRate = exchangeRate;
        }
    }
}