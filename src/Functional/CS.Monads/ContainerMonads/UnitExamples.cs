namespace CS.Monads.ContainerMonads;

public static class UnitExamples
{
    public static void Run()
    {
        // convert fahrenheit to celsius
       var fahrenheit = 100m;
       var celsius = fahrenheit.ToUnit()
           .Bind(x => new Unit<decimal>(x - 32)) 
           .Map(x => x * 5)
           .Map(x => x / 9)
           .Map(x => Math.Round(x, 2)).Value;
       
       Console.WriteLine($"{fahrenheit}°F --> {celsius}°C");
    }
}