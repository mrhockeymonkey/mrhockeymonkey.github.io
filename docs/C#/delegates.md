# Delegates
Delegates are pointers to methods that can be passed as variables.

A `delegate` keyword defines a delegate type or an anonymous function that can be converted to a delegate type.

An `Action<T>` delegate points to a method that takes one or more arguments but returns no value.

A `Func<T>` delegate points to a method that takes one of more arguments and returns a value.

A Predicate takes one or more arguments and returns a bool, i.e. `Func<T,bool>`

```c#
public static void SomeMethod(string str)
{
    Console.WriteLine(str);
}

public delegate void Del(string str);

static void Main(string[] args)
{
    // delegates
    Del namedDelegate = SomeMethod;
    namedDelegate("foo");

    Del lambdaDelegate = str => Console.WriteLine(str);
    lambdaDelegate("bar");
    
    // anon function, better to use lambda in most cases
    Del anonDelegate = delegate(string str) { Console.WriteLine(str); };
    anonDelegate("baz");

    // actions
    Action<string> actionMethod = new(SomeMethod);
    Action<string, string> actionLambda = (s1, s2) => Console.WriteLine($"{s1} {s2}");
    Action<string, string> actionAnon = delegate(string s, string s1) {  };

    actionMethod("hello");
    actionLambda.Invoke("hello", "world");

    // funcs
    Func<int, int> funcLambda = i => i * i;
    Console.WriteLine($"Func: {funcLambda(2)}");
}
```