using System;
using System.Globalization;
using BankSystem.Domain.Interfaces;

namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Base class representing a Bank Account.
    /// Implements core behaviors and formatting.
    /// </summary>
    public abstract class BankAccount : IAccount, ITransferable
    {
        protected decimal _balance;

        public virtual decimal Balance => _balance;

        public abstract string AccountType { get; }

        protected BankAccount(decimal initialBalance)
        {
            _balance = initialBalance;
        }

        /// <summary>
        /// Formats currency with dots (.) as thousand separators and a comma (,) as decimal separator.
        /// Guaranteed platform-independent formatting for VND currency.
        /// </summary>
        public static string FormatVnd(decimal amount)
        {
            var nfi = new NumberFormatInfo
            {
                NumberGroupSeparator = ".",
                NumberDecimalSeparator = ",",
                NumberGroupSizes = new[] { 3 }
            };
            return amount.ToString("N0", nfi);
        }

        public virtual void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {FormatVnd(Balance)} đ");
        }

        public virtual bool Transfer(decimal amount, out string message)
        {
            if (amount <= 0)
            {
                message = "Transfer amount must be greater than zero.";
                return false;
            }

            if (amount > _balance)
            {
                message = $"Insufficient balance. Current balance is: {FormatVnd(_balance)} đ";
                return false;
            }

            _balance -= amount;
            
            var formattedTransfer = FormatVnd(amount);
            var formattedBalance = FormatVnd(_balance);
            
            message = $"Your transferred {formattedTransfer} đ, Your balancer : {formattedBalance} đ";
            return true;
        }
    }
}
