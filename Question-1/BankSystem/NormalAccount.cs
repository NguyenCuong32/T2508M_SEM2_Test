namespace BankSystem;

// Normal account: balance stored directly in VND
public class NormalAccount : AccountBase
{
    public NormalAccount(string accountId, string ownerName, decimal balanceVND)
        : base(accountId, ownerName, balanceVND) { }

    // Balance = raw VND stored
    public override decimal GetBalance() => _balance;
}
