namespace ppc.Interface
{
    public interface IExchange : IAccount
    {
        decimal ExchangeRate { get; }
    }
}
