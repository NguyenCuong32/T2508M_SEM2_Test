namespace BankSystem;

// SRP: Only responsibility = format currency
public static class CurrencyFormatter
{
    public static string FormatVND(decimal amount)
    {
        // Format: 25.000.000 đ  (Vietnamese dot-separated thousands)
        return string.Format("{0:N0}", amount).Replace(",", ".") + " đ";
    }
}
