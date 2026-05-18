namespace BankSystem;

// OCP: Open for extension (subclass), Closed for modification
// LSP: Subclasses can substitute base without breaking behavior
public abstract class AccountBase : IAccount
{
    public string AccountId { get; }
    public string OwnerName { get; }
    protected decimal _balance;

    protected AccountBase(string accountId, string ownerName, decimal initialBalance)
    {
        AccountId   = accountId;
        OwnerName   = ownerName;
        _balance    = initialBalance;
    }

    // Template Method pattern: subclasses define HOW balance is computed
    public abstract decimal GetBalance();

    public void ShowBalance()
    {
        Console.WriteLine($"Your balance: {CurrencyFormatter.FormatVND(GetBalance())}");
    }

    public void Transfer(decimal amount)
    {
        decimal currentBalance = GetBalance();

        if (amount <= 0)
        {
            Console.WriteLine("Transfer amount must be > 0.");
            return;
        }

        if (amount > currentBalance)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }

        _balance -= amount;
        Console.WriteLine($"Your transferred {CurrencyFormatter.FormatVND(amount)}, " +
                          $"Your balance: {CurrencyFormatter.FormatVND(GetBalance())}");
    }
}
