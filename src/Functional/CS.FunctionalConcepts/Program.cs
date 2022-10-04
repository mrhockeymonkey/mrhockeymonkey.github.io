// See https://aka.ms/new-console-template for more information

using CS;

Console.WriteLine("Hello, World!");

// c# functions take any number of arguments
Func<int, int, int> add = (x, y) => x + y;
var r1 = add(2, 2);
Console.WriteLine($"2 plus 2 is {r1}");

// curried functions take one argument but are composable with other functions
Func<int,Func<int, int>> curriedAdd = (x) => y => x + y;
Func<int,int> plus2 = curriedAdd(2);
var r2 = plus2(2);
Console.WriteLine($"2 plus 2 is still {r2}");

// extensions for conversion
Func<int, Func<int, int>> curriedAddAgain = add.Curry();
Func<int, int, int> addAgain = curriedAdd.UnCurry();

// partial functions
Func<int,int> plus2Again = add.Partial(2);
Console.WriteLine($"2 plus 2 is yet again {plus2Again(2)}");