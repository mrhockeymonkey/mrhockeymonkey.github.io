// See https://aka.ms/new-console-template for more information

using TaxCalculator;
using static System.Decimal;

Console.WriteLine("Hello, World!");

// Personal Allowance	Up to £12,570	0% 
// Basic rate	£12,571 to £50,270	20%
// Higher rate	£50,271 to £150,000	40%
// Additional rate	over £150,000	45%

var (band1, band2, band3) = (12_570m, 50_270m, 150_000m);

var functionalCalculator = new FunctionalTaxCalculator(new Func<decimal, decimal>[]
{
    x => x >= band1 ? (Min(x, band2) - band1) * 0.2m : 0m,
    x => x >= band2 ? (Min(x, band3) - band2) * 0.4m : 0m,
    x => x >= band3 ? (x - band3) * 0.45m : 0m,
}); 

Console.WriteLine($"{functionalCalculator.Calculate(10_000m)} should be £0"); 
Console.WriteLine($"{functionalCalculator.Calculate(16_000m)} should be £686"); 
Console.WriteLine($"{functionalCalculator.Calculate(65_000m)} should be £13,432");
Console.WriteLine($"{functionalCalculator.Calculate(250_000m)} should be £92,432");

var objectCalculator = new ObjectTaxCalculator(new TaxBand[]
{
    new(band1, band2, 0.2m),
    new(band2, band3, 0.4m),
    new(band3, MaxValue, 0.45m)
});

Console.WriteLine($"{objectCalculator.Calculate(10_000m)} should be £0"); 
Console.WriteLine($"{objectCalculator.Calculate(16_000m)} should be £686"); 
Console.WriteLine($"{objectCalculator.Calculate(65_000m)} should be £13,432");
Console.WriteLine($"{objectCalculator.Calculate(250_000m)} should be £92,432");