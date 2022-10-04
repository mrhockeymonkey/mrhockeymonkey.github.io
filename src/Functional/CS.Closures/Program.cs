// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;

Console.WriteLine("Hello, World!");

// a closure captures a reference to a variable not within its own scope
string freeVariable = "Hello, ";
Func<string,string> sayHello = value => freeVariable + value;
Console.WriteLine(sayHello("Moon"));

// when in a multithreaded environment care must be taken with captured variables
// it is the ref that is captured not the value
for (var i = 1; i <= 3; i++)
{
    Task.Factory.StartNew(() => Console.WriteLine("{0} - {1}", Thread.CurrentThread.ManagedThreadId, i));
}
await Task.Delay(1000);
// 3 - 4
// 5 - 4
// 6 - 4
// the ref was incremented 3 times before the thread started

string str = "i am 0";
for (var i = 1; i <= 3; i++)
{
    var localStr = $"i really am {i}"; // use can create and capture a temporary var
    Task.Factory.StartNew(() =>
    {
        str = $"i am {i}"; // again because i is captured ref it is shared by other threads
        Console.WriteLine("{0} - {1} but {2}", Thread.CurrentThread.ManagedThreadId, str, localStr);
    });
}
await Task.Delay(1000);
// 8 - i am 4 but i really am 1
// 5 - i am 4 but i really am 2
// 9 - i am 4 but i really am 3

// another way to safely work in parallel is to use immutable variables
List<int> list = new List<int>() { 1, 2, 3 };
ImmutableList<int> immutableList = list.ToImmutableList();

var append4Task = Task.Factory.StartNew(() =>
{
    list.Add(4);
    var newImmutabaleList = immutableList.Add(4); // immutability forces you to create a new obj
});
var append5Task = Task.Factory.StartNew(() =>
{
    list.Add(5);
    var newImmutabaleList = immutableList.Add(5); // immutability forces you to create a new obj
});
Console.WriteLine($"list is now: {string.Join(',', list)}");
Console.WriteLine($"immutable list is still {string.Join(',', immutableList)}");
// list is now: 1,2,3,4,5
// immutable list is still 1,2,3

