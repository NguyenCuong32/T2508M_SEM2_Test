namespace BankAccountConsole.Services;

public sealed class ExchangeRateConverter : IExchangeRateConverter
{
    public decimal ConvertToVnd(decimal amount, decimal exchangeRate)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        if (exchangeRate <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(exchangeRate), "Exchange rate must be greater than zero.");
        }

        return amount * exchangeRate;
    }
}

