namespace BankAccountConsole.Accounts;

public abstract class BankAccount : IBankAccount
{
    private decimal _balance;

    protected BankAccount(string accountName, decimal openingBalance)
    {
        ValidatePositive(openingBalance, nameof(openingBalance));
        AccountName = accountName;
        _balance = openingBalance;
    }

    public string AccountName { get; }

    public decimal CheckBalance()
    {
        return _balance;
    }

    public TransferResult Transfer(decimal amount)
    {
        ValidatePositive(amount, nameof(amount));

        if (amount > _balance)
        {
            throw new InvalidOperationException("Transfer amount cannot be greater than current balancer.");
        }

        _balance -= amount;
        return new TransferResult(amount, _balance);
    }

    protected static void ValidatePositive(decimal amount, string parameterName)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Amount must be greater than zero.");
        }
    }
}

