## Closure

Objects are data with behaviour

```c#
public class Foo 
{
    public int _number;

    Foo(int number)
    {
        // object created, number IS the data...
        _number = number;
    }

    public int IncBy(int n)
    {
        // ... with behaviour
        _number = _number + n;
        return _number;
    }
}
```

Closures are behaviour with data

```c#
int number = 1;

// this function is "pure" behaviour...
Func<int, int> IncBy = n =>
{
    //... with data
    number = number + n;
    return number
};
```

The way the compiler handles a closure above would be ....?

