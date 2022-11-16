// See https://aka.ms/new-console-template for more information

using ConcurrentCircularBuffer;

Console.WriteLine("Hello, World!");

var circularArray = new Cas2BasedCircularArray<Foo>(512);
var work = () =>
{
    for (int i = 0; i < 1000; i++)
    {
        circularArray.Append(new Foo
        {
            Value = i,
            ThreadId = Thread.CurrentThread.ManagedThreadId
        });
    }
};

var tf = new TaskFactory();
Parallel.Invoke(work);


public class Foo
{
    public int Value { get; set; }
    public int ThreadId { get; set; }

    public override string ToString() => $"{Value} from thread {ThreadId}";
}