namespace TaxCalculator;

public class ObjectTaxCalculator
{
    private readonly TaxBand[] _taxBands;

    public ObjectTaxCalculator(TaxBand[] taxBands)
    {
        _taxBands = taxBands;
    }

    public decimal Calculate(decimal netIncome) => _taxBands.Select(band => band.Calculate(netIncome)).Sum();
}

public record TaxBand(decimal Lower, decimal Upper, decimal Rate)
{
    public decimal Calculate(decimal amount)
    {
        if (amount < Lower) return 0m;
        return (decimal.Min(amount, Upper) - Lower) * Rate;
    }
}