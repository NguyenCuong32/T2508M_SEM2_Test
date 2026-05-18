using System;
using System.Text;

namespace BankSystem
{
    public interface IAccount
    {
        double Balance { get; }
        void CheckBalance();
        void BankTransfer(double amount);
    }

    public interface IExchangeable
    {
        void SetExchangeRate(double rate, double amount);
    }

    public abstract class BankAccount : IAccount
    {
        public double Balance { get; protected set; }

        protected BankAccount(double initialBalance)
        {
            Balance = initialBalance;
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {Balance:N0} đ");
        }

        public virtual void BankTransfer(double amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Error: Insufficient balance.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"Your transferred {amount:N0} đ, Your balancer : {Balance:N0} đ");
        }
    }

    public class NormalAccount : BankAccount
    {
        public NormalAccount(double initialBalance) : base(initialBalance)
        {
        }
    }

    public class ExchangeAccount : BankAccount, IExchangeable
    {
        public double ExchangeRate { get; private set; }
        public double ForeignAmount { get; private set; }

        public ExchangeAccount() : base(0)
        {
        }

        public void SetExchangeRate(double rate, double amount)
        {
            ExchangeRate = rate;
            ForeignAmount = amount;
            Balance = rate * amount; 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("=== HE THONG NGAN HANG ===");
            
            Console.WriteLine("\n[1] Normal Account:");
            Console.Write("Enter initial amount (VND): ");
            if (double.TryParse(Console.ReadLine(), out double nAmount))
            {
                IAccount normalAcc = new NormalAccount(nAmount);
                normalAcc.CheckBalance();

                Console.Write("Enter amount to transfer: ");
                if (double.TryParse(Console.ReadLine(), out double tAmount))
                {
                    normalAcc.BankTransfer(tAmount);
                }
            }

            Console.WriteLine("\n[2] Exchange Account:");
            ExchangeAccount exAcc = new ExchangeAccount();

            Console.Write("Enter Today Exchange Rate: ");
            double.TryParse(Console.ReadLine(), out double rate);

            Console.Write("Enter Your Amount (USD): ");
            double.TryParse(Console.ReadLine(), out double fAmount);

            exAcc.SetExchangeRate(rate, fAmount);
            
            IAccount exInterface = exAcc;
            exInterface.CheckBalance();

            Console.Write("Enter amount to transfer (VND): ");
            if (double.TryParse(Console.ReadLine(), out double exTransfer))
            {
                exInterface.BankTransfer(exTransfer);
            }

            Console.ReadKey();
        }
    }
}