namespace BankAccountSystem.Models
{
    class NormalAccount : Account
    {
        public NormalAccount(decimal amount)
        {
            Amount = amount;
            Balance = amount;
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"\nYour balance: {Balance:N0} đ");
        }
    }
}