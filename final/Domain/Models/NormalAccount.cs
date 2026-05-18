namespace BankSystem.Domain.Models
{
    /// <summary>
    /// Represents a standard bank account where transactions and balances are in VND directly.
    /// </summary>
    public class NormalAccount : BankAccount
    {
        public override string AccountType => "Normal Account";

        public NormalAccount(decimal balanceVnd) : base(balanceVnd)
        {
        }
    }
}
