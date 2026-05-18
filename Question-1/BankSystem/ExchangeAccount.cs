namespace BankSystem;

// Exchange account: amount stored in foreign currency (e.g. USD)
// Balance (VND) = ExchangeRate × Amount
public class ExchangeAccount : AccountBase, IExchangeable
{
    public decimal ExchangeRate { get; private set; }

    // _balance here = amount in foreign currency (e.g. USD)
    public decimal Amount => _balance;

    public ExchangeAccount(string accountId, string ownerName, decimal foreignAmount, decimal exchangeRate)
        : base(accountId, ownerName, foreignAmount)
    {
        ExchangeRate = exchangeRate;
    }

    // Balance = ExchangeRate × Amount  → result in VND
    public override decimal GetBalance() => ExchangeRate * _balance;

    // Update exchange rate (e.g. daily feed)
    public void UpdateExchangeRate(decimal newRate)
    {
        ExchangeRate = newRate;
        Console.WriteLine($"Exchange rate updated → {CurrencyFormatter.FormatVND(newRate)} / unit");
    }
}
