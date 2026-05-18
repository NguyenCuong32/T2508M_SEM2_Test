namespace Question1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">>>>>>>>>>> BANK SYSTEM <<<<<<<<<<");
            Console.WriteLine("1. Normal Account");
            Console.WriteLine("2. Exchange Account");

            Console.Write("Choose type: ");
            int choice = int.Parse(Console.ReadLine());

            IBankAccount account;

            if (choice == 1)
            {
                Console.Write("Enter balance: ");
                decimal balance = decimal.Parse(Console.ReadLine());

                account = new NormalAccount(balance);
            }
            else
            {
                Console.Write("Enter USD amount: ");
                decimal usd = decimal.Parse(Console.ReadLine());

                Console.Write("Enter exchange rate: ");
                decimal rate = decimal.Parse(Console.ReadLine());

                account = new ExchangeAccount(usd, rate);
            }

            Console.WriteLine();
            account.CheckBalance();

            Console.WriteLine();

            Console.Write("Enter transfer amount: ");
            decimal transfer = decimal.Parse(Console.ReadLine());

            account.Transfer(transfer);

            Console.ReadKey();
        }
    }
}