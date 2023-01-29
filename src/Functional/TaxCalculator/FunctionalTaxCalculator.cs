namespace TaxCalculator;

public class FunctionalTaxCalculator
{
    private readonly Func<decimal, decimal>[] _taxBandCalculations;


    public FunctionalTaxCalculator(Func<decimal, decimal>[] taxBandCalculations)
    {
        _taxBandCalculations = taxBandCalculations;
    }

    public decimal Calculate(decimal netIncome) => _taxBandCalculations.Select(func => func(netIncome)).ToList().Sum();
}