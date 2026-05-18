using System;
using BankApp;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== BANK ACCOUNT MANAGEMENT ===\n");

        // --- Normal Account ---
        Console.WriteLine(">> Normal Account (số dư ban đầu: 25,000,000 đ)");
        NormalAccount normal = new NormalAccount(25_000_000);
        normal.CheckBalance();

        Console.Write("Nhập số tiền muốn chuyển khoản: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            normal.Transfer(transferAmount);

        Console.WriteLine();

        // --- Exchange Account ---
        Console.WriteLine(">> Exchange Account (USD → VND, tỷ giá: 25,000)");
        Console.Write("Nhập số lượng USD: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal usdAmount))
        {
            decimal exchangeRate = 25_000m;
            ExchangeAccount exchange = new ExchangeAccount(usdAmount, exchangeRate);
            exchange.CheckBalance();

            Console.Write("Nhập số USD muốn chuyển khoản: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal usdTransfer))
                exchange.Transfer(usdTransfer);
        }

        Console.WriteLine("\nNhấn phím bất kỳ để thoát...");
        Console.ReadKey();
    }
}
