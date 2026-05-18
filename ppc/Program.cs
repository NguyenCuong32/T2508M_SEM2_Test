using System;
using ppc.Interface;
using ppc.Model;

namespace ppc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("===== BANK SYSTEM =====");
            
            Console.WriteLine("\n--- Exchange Account ---");
            Console.Write("Enter your amount (USD): ");
            string usdInput = Console.ReadLine();
            if (string.IsNullOrEmpty(usdInput)) usdInput = "1000";

            if (decimal.TryParse(usdInput, out decimal usdAmount))
            {
                decimal exchangeRate = 25000m;
                IAccount exchangeAccount = new ExchangeAccount(usdAmount, exchangeRate);
                
                exchangeAccount.CheckBalance();

                Console.Write("Enter transfer amount (VND): ");
                string transferInput = Console.ReadLine();
                if (string.IsNullOrEmpty(transferInput)) transferInput = "1000000";

                if (decimal.TryParse(transferInput, out decimal transferVND))
                {
                    exchangeAccount.Transfer(transferVND);
                }
            }

            Console.WriteLine("\n--- Normal Account ---");
            Console.Write("Enter your amount (VND): ");
            string vndInput = Console.ReadLine();
            if (string.IsNullOrEmpty(vndInput)) vndInput = "25000000";

            if (decimal.TryParse(vndInput, out decimal vndAmount))
            {
                IAccount normalAccount = new NormalAccount(vndAmount);
                
                normalAccount.CheckBalance();

                Console.Write("Enter transfer amount (VND): ");
                string transferInput = Console.ReadLine();
                if (string.IsNullOrEmpty(transferInput)) transferInput = "1000000";

                if (decimal.TryParse(transferInput, out decimal transferNormal))
                {
                    normalAccount.Transfer(transferNormal);
                }
            }
            
            Console.ReadLine();
        }
    }
}
