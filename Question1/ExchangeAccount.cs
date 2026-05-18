namespace Question1
{
    public class ExchangeAccount : BankAccount
    {
        private decimal ExchangeRate;
        private decimal UsdAmount;

        public ExchangeAccount(decimal usdAmount, decimal exchangeRate)
            : base(usdAmount * exchangeRate)
        {
            UsdAmount = usdAmount;
            ExchangeRate = exchangeRate;
        }

        public override void CheckBalance()
        {
            Console.WriteLine(
                $"Balance = {ExchangeRate:N0} x {UsdAmount:N0}"
            );

            Console.WriteLine(
                $"Your balance: {Balance:N0} đ"
            );
        }
    }
}