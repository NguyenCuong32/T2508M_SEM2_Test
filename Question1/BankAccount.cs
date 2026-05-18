namespace Question1
{
    public abstract class BankAccount : IBankAccount
    {
        protected decimal Balance;

        public BankAccount(decimal balance)
        {
            Balance = balance;
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balance: {Balance:N0} đ");
        }

        public virtual void Transfer(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Not enough money!");
                return;
            }

            Balance -= amount;

            Console.WriteLine(
                $"Your transferred {amount:N0} đ, Your balance: {Balance:N0} đ"
            );
        }
    }
}