using BankAccountSystem.Interfaces;

namespace BankAccountSystem.Models
{
    abstract class Account : ICheckBalance, ITransfer
    {
        public decimal Balance { get; set; }

        public decimal Amount { get; set; }

        public abstract void CheckBalance();
    }
}