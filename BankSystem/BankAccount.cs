namespace BankSystem
{
    public abstract class BankAccount : IAccount
    {
        public string AccountNumber { get; protected set; }
        public decimal Balance { get; protected set; }

        protected BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public abstract void CheckBalance();

        public virtual void Transfer(decimal amount)
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
            Console.WriteLine($"Your transferred {amount:N0} đ, Your balancer: {Balance:N0} đ");
        }

        protected string FormatCurrency(decimal amount) => $"{amount:N0} đ";
    }
}