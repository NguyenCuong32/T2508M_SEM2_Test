namespace BankApp
{
    public interface IAccount
    {
        void CheckBalance();
        void Transfer(decimal amount);
    }
}
