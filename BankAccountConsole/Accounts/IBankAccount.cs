namespace BankAccountConsole.Accounts;

public interface IBankAccount
{
    decimal CheckBalance();

    TransferResult Transfer(decimal amount);
}

