using ppc.Interface;

namespace ppc.Model
{
    public class ExchangeAccount : Account, IExchange
    {
        public decimal ExchangeRate { get; private set; }

        public ExchangeAccount(decimal foreignAmount, decimal exchangeRate)
        {
            ExchangeRate = exchangeRate;
            Balance = exchangeRate * foreignAmount;
        }
    }
}
