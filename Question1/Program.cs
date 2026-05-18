using System;
using System.Text;

namespace Question1
{
    
    public interface IAccountOperations
    {
        void CheckBalance();
        void BankTransfer(decimal transferAmount);
    }

   
    public abstract class Account : IAccountOperations
    {
        protected decimal Balance { get; set; }

        public Account(decimal balance)
        {
            Balance = balance;
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {Balance:N0} đ");
        }

        public virtual void BankTransfer(decimal transferAmount)
        {
            if (transferAmount > Balance)
            {
                Console.WriteLine("Số dư không đủ để chuyển khoản.");
                return;
            }

            Balance -= transferAmount;
            Console.WriteLine($"Your transferred {transferAmount:N0} đ, Your balancer : {Balance:N0} đ");
        }
    }

   
    public class NormalAccount : Account
    {
        public NormalAccount(decimal amount) : base(amount)
        {
        }
    }

    
    public class ExchangeAccount : Account
    {
        private decimal ExchangeRate { get; set; }

        public ExchangeAccount(decimal amount, decimal exchangeRate) 
            : base(amount * exchangeRate)
        {
            ExchangeRate = exchangeRate;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("--- Hệ Thống Tài Khoản Ngân Hàng ---");
            Console.WriteLine("Chọn loại tài khoản:");
            Console.WriteLine("1. Tài khoản thường (Normal Account)");
            Console.WriteLine("2. Tài khoản quy đổi (Exchange Account - VD: USD sang VND)");
            Console.Write("Nhập lựa chọn (1/2): ");
            
            string? choice = Console.ReadLine();

            Account? account = null;

            if (choice == "1")
            {
                Console.Write("Nhập số tiền của bạn (VND): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    account = new NormalAccount(amount);
                }
            }
            else if (choice == "2")
            {
                Console.Write("Nhập số tiền của bạn (VD: 1000 USD): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.Write("Nhập tỷ giá (VD: 25000 cho USD sang VND): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal rate))
                    {
                        account = new ExchangeAccount(amount, rate);
                    }
                }
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ.");
                return;
            }

            if (account == null)
            {
                Console.WriteLine("Số tiền nhập không hợp lệ.");
                return;
            }

            Console.WriteLine();
            
            account.CheckBalance();

           
            Console.Write("\nNhập số tiền muốn chuyển (VND): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                account.BankTransfer(transferAmount);
            }
            else
            {
                Console.WriteLine("Số tiền chuyển không hợp lệ.");
            }

            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}
