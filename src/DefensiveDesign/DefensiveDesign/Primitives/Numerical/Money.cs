using System.ComponentModel;
using System.Reflection;

namespace Primitives.Numerical;

public class Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    public bool CanAdd(Money other) => Currency == other.Currency;
}

public class PositiveMoney : Money
{
    public PositiveMoney(decimal amount, Currency currency) : base(amount, currency)
    {
        if (amount <= 0)
            throw new ArgumentException();
    }
}

public class Currency
{
    
}