namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Represents an exchange-based bank account where the balance is calculated using an exchange rate and foreign currency amount.
    /// Formula: Balance = ExchangeRate * Amount.
    /// </summary>
    public class ExchangeAccount : BankAccount
    {
        public override string AccountType => "Exchange Account";
        
        public decimal OriginalAmount { get; }
        public decimal ExchangeRate { get; }

        public ExchangeAccount(decimal amountUsd, decimal exchangeRate) 
            : base(amountUsd * exchangeRate)
        {
            OriginalAmount = amountUsd;
            ExchangeRate = exchangeRate;
        }
    }
}
