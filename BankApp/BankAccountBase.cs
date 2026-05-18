using System;

namespace BankApp
{
    public abstract class BankAccountBase : IAccount
    {
        protected decimal Balance { get; set; }

        protected BankAccountBase(decimal initialBalance)
        {
            Balance = initialBalance;
        }

        // Template method — subclasses define how balance is computed
        public abstract void CheckBalance();

        public virtual void Transfer(decimal amount)
        {
            Balance -= amount;
            Console.WriteLine(
                $"Your transferred {amount:N0} đ, Your balancer : {Balance:N0} đ"
            );
        }
    }
}
