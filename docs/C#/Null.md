# Null
is evil.

## Null Object Pattern
Where possible create an object that cant stand in place of null and has null-like or no behaviour in the context of the business logic

```c#
// making class a singleton for better performance
public class VoidWarranty : IWarrany
{
    [ThreadStatic]
    private static VoidWarranty _instance;

    private VoidWarranty() { }

    public static VoidWarranty => 
    {
        if (_instance == null)
        {
            _instance = new VoidWarranty();
        }
        return _instance;
    }

    // this handles the behaviour "as if" the warranty was null
    // i.e. do nothing
    public void Claim(DateTime date, Action onValidClaim) { }
}
```

## Option Pattern
A lightweight implementation

```c#
class Option<T> : IEnumerable<T>
{
    private IEnumerable<T> Content { get; }

    private Option(IEnumerable<T> content)
    {
        this.Content = content;
    }

    public static Option<T> Some(T value) => new Option<T>(new[] {value});

    public static Option<T> None() => new Option<T>(new T[0]);

    public IEnumerator<T> GetEnumerator() => this.Content.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
```
```c#
static class EnumerableExtensions
{
    public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        foreach (T obj in sequence)
            action(obj);
    }
}
```

For more heavy weight implementation:

- https://github.com/nlkl/Optional
- https://github.com/louthy/language-ext#optional-and-alternative-value-monads