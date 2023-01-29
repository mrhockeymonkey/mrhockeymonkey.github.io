namespace Construction;

public class SimpleCoffee : ICoffee
{
    public string Name { get; }

    public SimpleCoffee(string name)
    {
        Name = name;
    }
}