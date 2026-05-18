using System;

namespace BankApp
{
    public class ExchangeAccount : BankAccountBase
    {
        private readonly decimal _exchangeRate;

        // balance = exchangeRate * amount (amount = Balance trước quy đổi)
        public ExchangeAccount(decimal amount, decimal exchangeRate)
            : base(amount * exchangeRate)
        {
            _exchangeRate = exchangeRate;
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {Balance:N0} đ");
        }

        public override void Transfer(decimal amount)
        {
            // amount truyền vào là số tiền theo đơn vị gốc → quy đổi trước
            decimal convertedAmount = amount * _exchangeRate;
            Balance -= convertedAmount;
            Console.WriteLine(
                $"Your transferred {convertedAmount:N0} đ, Your balancer : {Balance:N0} đ"
            );
        }
    }
}
