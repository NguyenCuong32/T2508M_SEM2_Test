namespace BankAccountConsole.Accounts;

public sealed class NormalAccount : BankAccount
{
    public NormalAccount(decimal openingBalance)
        : base("Normal Account", openingBalance)
    {
    }
}

