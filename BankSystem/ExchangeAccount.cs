namespace BankSystem
{
    public class ExchangeAccount : BankAccount, IExchangeable
    {
        public decimal ExchangeRate { get; private set; }
        public string Currency { get; private set; }

        public ExchangeAccount(string accountNumber, decimal initialBalance, decimal exchangeRate, string currency)
            : base(accountNumber, initialBalance)
        {
            ExchangeRate = exchangeRate;
            Currency = currency;
        }

        public decimal GetExchangedBalance() => Balance * ExchangeRate;

        public override void CheckBalance()
        {
            decimal vnd = GetExchangedBalance();
            Console.WriteLine($"Your balancer: {FormatCurrency(vnd)}");
        }

        public override void Transfer(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Số tiền chuyển khoản không hợp lệ!");
                return;
            }
            if (amount > Balance)
            {
                Console.WriteLine("Số dư không đủ để thực hiện giao dịch!");
                return;
            }

            Balance -= amount;
            decimal vndTransferred = amount * ExchangeRate;
            decimal vndBalance = GetExchangedBalance();

            Console.WriteLine($"Your transferred {vndTransferred:N0} đ, Your balancer: {FormatCurrency(vndBalance)}");
        }
    }
}