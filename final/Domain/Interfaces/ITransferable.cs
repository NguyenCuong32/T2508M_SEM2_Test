namespace BankSystem.Domain.Interfaces
{
    /// <summary>
    /// Defines the bank transfer behavior for transferable accounts.
    /// Segregated from IAccount to support ISP.
    /// </summary>
    public interface ITransferable
    {
        bool Transfer(decimal amount, out string message);
    }
}
