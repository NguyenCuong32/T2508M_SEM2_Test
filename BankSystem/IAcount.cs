namespace BankSystem
{
    public interface IAccount
    {
        void CheckBalance();
        void Transfer(decimal amount);
    }
}