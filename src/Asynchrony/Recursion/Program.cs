// See https://aka.ms/new-console-template for more information

using System.Numerics;
using Recursion;
using Shared;

Console.WriteLine("Hello, World!");

Console.WriteLine(Factorial(4));

Console.WriteLine(FactorialLinearRecursive(4));

// throws stack overflow because c# compiler does not optimize for tail recursion
// Console.WriteLine(FactorialTailRecursive(20000, 1));

// Console.WriteLine(FactorialTailRecursiveOptimized(20000, 1));

var t= ExecutionTime.Measure(() =>
{
    BigInteger result = TailRecursion.Execute(() => FactorialTail(50000, 1));
    Console.WriteLine(result);
});
Console.WriteLine($"{t.TotalSeconds} seconds");


static int Factorial(int n)
{
    int result = 1;

    for (int i = 1; i <= n; i++)
        result *= i;

    return result;
}

static int FactorialLinearRecursive(int n)
{
    if (n <= 1) // Exit condition
        return 1;

    return n * Factorial(n - 1);
}


static BigInteger FactorialTailRecursive(int n, BigInteger product)
{
    if (n <= 1)
        return product;
    return FactorialTailRecursive(n - 1, n * product);
}

static BigInteger FactorialTailRecursiveOptimized(int n, BigInteger product)
{
    var hof = Trampoline.MakeTrampoline((int n, BigInteger product) =>
    {
        if (n <= 1)
            return Trampoline.ReturnResult<int, BigInteger, BigInteger>(product);
        return Trampoline.Recurse<int, BigInteger, BigInteger>(n - 1, n * product);
    });

    return hof(n, product);
}


RecursionResult<BigInteger> FactorialTail(int n, BigInteger product)
{
    if (n < 2)
        return TailRecursion.Return(product);
    return TailRecursion.Next(() => FactorialTail(n - 1, n * product));
}
