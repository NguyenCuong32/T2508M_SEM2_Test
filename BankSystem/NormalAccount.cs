namespace BankSystem
{
    public class NormalAccount : BankAccount
    {
        public NormalAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance) { }

        public override void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {FormatCurrency(Balance)}");
        }
    }
}