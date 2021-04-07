# Asynchrony

I/O-bound: Your code will be "waiting" for something, such as data from a database. Because there is now "work" to do creating a new thread for this is inefficient

CPU-bound: Your code will be performing an expensive computation. In this case a new thread makes sense

## Task Based

The `async` keyword enables await.

The `await` keyword effectively pauses and waits for the awaitable, meaning no work is carried out, but the thread is no blocked so it is an "asynchronous wait".

The type `Task` is awaitable which is why 99% of the time you return a Task or Task<T>.

When a task is completed the remainder of the async method resumes on a context captured at the time.
- If you’re on a UI thread, then it’s a UI context.
- If you’re responding to an ASP.NET request, then it’s an ASP.NET request context.
- Otherwise, it’s usually a thread pool context (so it "can" be in a different thread)

The above is acheived because asp.net and Winforms extends `SynchronizationContext`, the base of which simple calls ThreadPool. 

```c#
// constructing tasks
var t = new Task(() => {}); // Task(Action)
t.Start();

var state = "foo";
var t = new Task((state) => {}) // Task(Action, State)
t.Start();

// create a list of tasks and wait for all. Good for I/O bound work (because it doesnt create dedicated threads)
var getUserTasks = new List<Task<User>>();
foreach (int userId in userIds)
{
    getUserTasks.Add(GetUserAsync(userId)); 
}
return await Task.WhenAll(getUserTasks);

// same with linq but linq
var getUserTasks = userIds.Select(id => GetUserAsync(id));
return await Task.WhenAll(getUserTasks);

// This queues up the work on the threadpool. Good for CPU-bound work (creates dedicated thread)
// DoExpensiveCalculation should be synchronours allowing the caller to decide how to handle it
var expensiveResultTask = Task.Run(() => DoExpensiveCalculation(data), cancellationToken);

// Task.Run() is a shortcut to
Task.Factory.StartNew(() => {});

// Yield forces method to return to caller and run async
// good for making an sychronous method (background service) async
// it will still run on the main thread but at a later time to keep it responsive
await Task.Yield();

// To force synchronous, i.e. block the thread
task.Wait(); // throws AggregateException
task.Result;
task.GetAwaiter().GetResult(); // throws the actual exception

// to bypass the default synchronization context
// redundant in asp.net core
.ConfigureAwait(false);
//if you’re writing app-level code, do not use ConfigureAwait(false)
//if you’re writing general-purpose library code, use ConfigureAwait(false)
//becuase it doesnt need to interacts with the app model like asp.net
//but be aware of delgates being passed to the library as that would class as app-domain code. 
```


## Event Based
You can define your own event handler but M$ provide two defaults
```c#
public delegate void EventHandler( object sender, EventArgs e); 
public delegate void EventHandler < TEventArgs >( object sender, TEventArgs e);
```

You define an event. Client code then subscribes to the event and handles it as required
```c#
class Person 
{
    // the keyword event simply stops client code from replacing
    // already subscribed delegates
    public event EventHandler<f> Shout;

    public void Poke(){
        // if someone is listening
        if (Shout != null){
            // then call the delegate
            Shout(this, EventArgs.Empty) // sender, args
        }
    }
}

// method must match signature
private static void HandleShout(object sender, EventArgs e)
{
    var person = sender as Person;
    Console.Writeline("Has been poked")
}

var harry = new Person();
// can assign multiple delegates to the event
harry.Shout += HandleShout;
harry.Poke();
```