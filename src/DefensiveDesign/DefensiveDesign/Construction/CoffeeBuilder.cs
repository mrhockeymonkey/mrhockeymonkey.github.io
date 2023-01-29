namespace Construction;

public class CoffeeBuilder
{
    private string _name;
    private string[] _validateNames = new[] { "Flat White", "Americano", "Espresso" };

    public void SetName(string name) => _name = name;
    
    // other builder methods

    // object preconditions defined here instead of in object ctor. 
    public bool CanBuild() =>
        !string.IsNullOrEmpty(_name) &&
        _validateNames.Contains(_name);

    public ICoffee Build()
    {
        if (!CanBuild())
            throw new InvalidOperationException();

        return new SimpleCoffee(_name);
    }

    // returns a factory method that can be invoked at a later time 
    public Func<ICoffee> GetCoffeeFactory(string name)
    {
        var builder = new CoffeeBuilder();
        builder.SetName(name);

        // here we can check the function will succeed in the future or fail fast
        if (!builder.CanBuild())
            throw new ArgumentException();

        return builder.Build;
    }
}