namespace BankAccountConsole.Services;

public interface IExchangeRateConverter
{
    decimal ConvertToVnd(decimal amount, decimal exchangeRate);
}

