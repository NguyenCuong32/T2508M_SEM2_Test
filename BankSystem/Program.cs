using System;

namespace BankSystem;

public interface IAccount
{
    decimal CheckBalance();
    void BankTransfer(decimal amount);
}

public class NormalAccount : IAccount
{
    private decimal balance;

    public NormalAccount(decimal amount) => balance = amount;

    public decimal CheckBalance() => balance;

    public void BankTransfer(decimal amount)
    {
        if (balance >= amount) balance -= amount;
    }
}

public class ExchangeAccount : IAccount
{
    private decimal amount;
    private decimal exchangeRate;

    public ExchangeAccount(decimal amount, decimal exchangeRate)
    {
        this.amount = amount;
        this.exchangeRate = exchangeRate;
    }

    public decimal CheckBalance() => amount * exchangeRate;

    public void BankTransfer(decimal amountVnd)
    {
        if (CheckBalance() >= amountVnd) amount -= (amountVnd / exchangeRate);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var culture = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

        Console.Write("Enter your amount: ");
        decimal amount = decimal.Parse(Console.ReadLine() ?? "0");
        
        Console.WriteLine("Account type (1. Normal, 2. Exchange): ");
        string type = Console.ReadLine() ?? "1";

        IAccount account = type == "2" ? new ExchangeAccount(amount, 25000) : new NormalAccount(amount);

        Console.WriteLine($"Your balancer: {account.CheckBalance().ToString("N0", culture)} đ");

        Console.Write("Enter transfer amount: ");
        decimal transferAmount = decimal.Parse(Console.ReadLine() ?? "0");
        
        account.BankTransfer(transferAmount);
        
        Console.WriteLine($"Your transferred {transferAmount.ToString("N0", culture)} đ, Your balancer : {account.CheckBalance().ToString("N0", culture)} đ");
    }
}
