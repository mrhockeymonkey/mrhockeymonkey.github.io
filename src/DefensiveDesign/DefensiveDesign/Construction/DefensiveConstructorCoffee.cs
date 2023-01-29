namespace Construction;

public class DefensiveConstructorCoffee
{
    private readonly string _name;

    public DefensiveConstructorCoffee(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        _name = name;
    }
}