using System;

namespace BankSystem.Domain.Interfaces
{
    /// <summary>
    /// Defines core bank account properties and actions.
    /// Follows Interface Segregation Principle (ISP) by keeping the interface focused on balance.
    /// </summary>
    public interface IAccount
    {
        decimal Balance { get; }
        string AccountType { get; }
        void CheckBalance();
    }
}
