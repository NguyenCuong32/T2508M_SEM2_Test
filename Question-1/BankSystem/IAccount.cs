namespace BankSystem;

// ISP: Split into focused interfaces
public interface IBalanceChecker
{
    decimal GetBalance();
    void ShowBalance();
}

public interface IBankTransfer
{
    void Transfer(decimal amount);
}

public interface IExchangeable
{
    decimal ExchangeRate { get; }
    decimal Amount { get; }
}

// Marker interface for all account types
public interface IAccount : IBalanceChecker, IBankTransfer
{
    string AccountId { get; }
    string OwnerName { get; }
}
