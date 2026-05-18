using System;

namespace BankApp
{
    public class NormalAccount : BankAccountBase
    {
        public NormalAccount(decimal initialBalance) : base(initialBalance) { }

        public override void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {Balance:N0} đ");
        }
    }
}
