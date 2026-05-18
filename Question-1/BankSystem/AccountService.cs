namespace BankSystem;

// DIP: High-level module depends on IAccount abstraction, not concrete types
public class AccountService
{
    private readonly IAccount _account;

    public AccountService(IAccount account)
    {
        _account = account;
    }

    public void CheckBalance() => _account.ShowBalance();

    public void MakeTransfer(decimal amount) => _account.Transfer(amount);

    public string GetAccountInfo()
        => $"[{_account.AccountId}] Owner: {_account.OwnerName}";
}
