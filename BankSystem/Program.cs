using BankSystem;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("==============================================");
            Console.WriteLine("    HỆ THỐNG QUẢN LÝ TÀI KHOẢN NGÂN HÀNG   ");
            Console.WriteLine("==============================================\n");

            bool running = true;
            while (running)
            {
                Console.WriteLine("Chọn loại tài khoản:");
                Console.WriteLine("  1. Tài khoản thường (Normal Account)");
                Console.WriteLine("  2. Tài khoản ngoại tệ (Exchange Account)");
                Console.WriteLine("  0. Thoát");
                Console.Write("Lựa chọn: ");

                switch (Console.ReadLine())
                {
                    case "1": RunNormalAccount();   break;
                    case "2": RunExchangeAccount(); break;
                    case "0": running = false; Console.WriteLine("Tạm biệt!"); break;
                    default:  Console.WriteLine("Lựa chọn không hợp lệ!\n"); break;
                }
            }
        }

        static void RunNormalAccount()
        {
            Console.WriteLine("\n--- TÀI KHOẢN THƯỜNG ---");
            Console.Write("Nhập số dư ban đầu (VND): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal balance) || balance < 0)
            {
                Console.WriteLine("Số tiền không hợp lệ!\n");
                return;
            }
            var service = new AccountService(new NormalAccount("ACC001", balance));
            RunAccountMenu(service);
        }

        static void RunExchangeAccount()
        {
            Console.WriteLine("\n--- TÀI KHOẢN NGOẠI TỆ ---");
            Console.Write("Nhập số dư ban đầu (USD): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal balance) || balance < 0)
            {
                Console.WriteLine("Số tiền không hợp lệ!\n");
                return;
            }
            Console.Write("Nhập tỷ giá USD → VND (VD: 25000): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal rate) || rate <= 0)
            {
                Console.WriteLine("Tỷ giá không hợp lệ!\n");
                return;
            }
            var service = new AccountService(new ExchangeAccount("ACC002", balance, rate, "USD"));
            RunAccountMenu(service);
        }

        static void RunAccountMenu(AccountService service)
        {
            bool inAccount = true;
            while (inAccount)
            {
                Console.WriteLine("\nChọn thao tác:");
                Console.WriteLine("  1. Kiểm tra số dư");
                Console.WriteLine("  2. Chuyển khoản");
                Console.WriteLine("  3. Quay lại");
                Console.Write("Lựa chọn: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        service.ShowBalance();
                        break;
                    case "2":
                        Console.Write("Nhập số tiền cần chuyển: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine();
                            service.DoTransfer(amount);
                        }
                        else Console.WriteLine("Số tiền không hợp lệ!");
                        break;
                    case "3":
                        inAccount = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
            Console.WriteLine();
        }
    }
}