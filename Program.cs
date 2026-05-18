using System;
namespace AssignmentCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("\n----- MENU BÀI THI C# -----");
                Console.WriteLine("1. Kiểm tra tài khoảng ngân hàng");
                Console.WriteLine("2. Hiển thị danh sách sản phẩm từ file JSON");
                Console.WriteLine("3. Thoát chương trình");
                Console.Write("Chọn chức năng (1-3): ");
                 string? choice = Console.ReadLine();
                  switch (choice)
                 {
                    case "1":
                        Muc1();
                        break;
                    case "2":
                        ProductService.DisplayProducts();
                        break;
                    case "3":
                        Console.WriteLine("Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                        break;
                }
            }
        }

        static void Muc1()
        {
            Console.WriteLine("\n--- KIỂM TRA CÂU 1 ---");
            Console.Write("Nhập số tiền gốc cho tài khoản (Exchange: nhập ngoại tệ, Normal: nhập VND): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Số tiền không hợp lệ.");
                return;
            }

            Console.WriteLine("\n--- Chọn loại tài khoản để Test ---");
            Console.WriteLine("a. Normal Account");
            Console.WriteLine("b. Exchange Account (Tỷ giá mặc định 25.000đ)");
            Console.Write("Chọn (a/b): ");
            string? type = Console.ReadLine()?.ToLower();

            IAccount account;

            if (type == "b")
            {
                account = new ExchangeAccount(amount, 25000);
            }
            else
            {
                account = new NormalAccount(amount);
            }

            // Gọi hàm show thông tin số dư theo mẫu của đề
            account.CheckBalance();

            Console.Write("\nNhập số tiền bạn muốn chuyển khoản thử nghiệm (VND): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                account.BankTransfer(transferAmount);
            }
        }
    }
}