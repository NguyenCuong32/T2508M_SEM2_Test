using System.Globalization;
using System.Text;
using BankAccountConsole.Accounts;
using BankAccountConsole.Services;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Bank Account System");
Console.WriteLine("-------------------");

BankAccount account = CreateAccount();

Console.WriteLine();
Console.WriteLine($"Your balancer: {CurrencyFormatter.FormatVnd(account.CheckBalance())}");

decimal transferAmount = ReadPositiveDecimal("Enter transfer amount (VND): ");

try
{
    TransferResult result = account.Transfer(transferAmount);
    Console.WriteLine(
        $"Your transferred {CurrencyFormatter.FormatVnd(result.TransferredAmount)}, Your balancer: {CurrencyFormatter.FormatVnd(result.RemainingBalance)}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

static BankAccount CreateAccount()
{
    IExchangeRateConverter exchangeRateConverter = new ExchangeRateConverter();

    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Choose account type:");
        Console.WriteLine("1. Normal Account");
        Console.WriteLine("2. Exchange Account");
        Console.Write("Your choice: ");

        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            decimal amount = ReadPositiveDecimal("Enter amount (VND): ");
            return new NormalAccount(amount);
        }

        if (choice == "2")
        {
            decimal amount = ReadPositiveDecimal("Enter amount (USD): ");
            decimal exchangeRate = ReadPositiveDecimal("Enter exchange rate (VND/USD, default 25000): ", 25_000m);
            return new ExchangeAccount(amount, exchangeRate, exchangeRateConverter);
        }

        Console.WriteLine("Please choose 1 or 2.");
    }
}

static decimal ReadPositiveDecimal(string prompt, decimal? defaultValue = null)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input) && defaultValue.HasValue)
        {
            return defaultValue.Value;
        }

        if (TryParseMoney(input, out decimal value) && value > 0)
        {
            return value;
        }

        Console.WriteLine("Please enter a positive number.");
    }
}

static bool TryParseMoney(string? input, out decimal value)
{
    value = 0m;

    if (string.IsNullOrWhiteSpace(input))
    {
        return false;
    }

    return decimal.TryParse(input, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out value)
        || decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
}

