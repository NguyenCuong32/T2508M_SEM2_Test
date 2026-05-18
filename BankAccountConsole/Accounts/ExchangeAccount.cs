using BankAccountConsole.Services;

namespace BankAccountConsole.Accounts;

public sealed class ExchangeAccount : BankAccount
{
    public ExchangeAccount(decimal foreignAmount, decimal exchangeRate, IExchangeRateConverter exchangeRateConverter)
        : base(
            "Exchange Account",
            (exchangeRateConverter ?? throw new ArgumentNullException(nameof(exchangeRateConverter)))
                .ConvertToVnd(foreignAmount, exchangeRate))
    {
        ValidatePositive(foreignAmount, nameof(foreignAmount));
        ValidatePositive(exchangeRate, nameof(exchangeRate));

        ForeignAmount = foreignAmount;
        ExchangeRate = exchangeRate;
    }

    public decimal ForeignAmount { get; }

    public decimal ExchangeRate { get; }
}
