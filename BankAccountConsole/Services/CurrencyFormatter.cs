using System.Globalization;

namespace BankAccountConsole.Services;

public static class CurrencyFormatter
{
    public static string FormatVnd(decimal amount)
    {
        string formattedAmount = amount.ToString("#,0", CultureInfo.InvariantCulture).Replace(',', '.');
        return $"{formattedAmount}đ";
    }
}

