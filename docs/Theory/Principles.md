OO organizes programs as objects: data structures consisting of datafields and methods together with their interactions.
OO is about bring operations closer to data
this pointer is silently passed with a call to an instance level function, so each function has an object on which it operates

dynamic dispatch - allows to substitute one concrete implementation to another at runtime without disturbing the caller

this is in contrast to  ...

procedural and structured programming - specifies the steps a program must take to reach a desired state.
functional programming - treats programs as evaluating mathematical functions and avoids state and mutable data.



# Principles

## S.O.L.I.D.
- **Single Responsibility**: A class should only have a single responsibility and thus on one reason to change.
- **Open-Closed Principle**: Entities should be open for extension but closed for modification.
- **Liskov Substitution**: Objects should be replaceable by their subtypes without altering the correctness of the program
- **Interface Segregation Principle**: Many smaller, specific interfaces are better than one general purpose one. 
- **Dependency Inversion Principle**: High-level modules should not depend on low-level modules. Both should depend on abstractions (interfaces), Abstractions should not depend on details. Details (concrete implementations) should depend on abstractions.

### Notes on Dependency Inversion Principle
One approach when applying the dependency inversion principle the higher lever code should "own" the interface, not the lower level module. This is where the "inversion" comes in. This architecture groups the higher/policy components and the abstractions that define lower services together in the same package. Upper layers can then use other implementations of the lower services.

Another approach when the lower-level layer components are closed or when the application requires the reuse of existing services, it is common that an Adapter mediates between the services and the abstractions.

## Encapsulation
Information Hiding (which is more like implementation hiding). You can expose information but you should hide the implementation details.

Invariant: Something that is true for all instances of a class.
Precondition: Something that must be true before an operation is invoked (to guarantee that it will work correctly). Users need/should meet such conditions before calling an operation
Postcondition: Something that must be true after an operation is invoked. Condition is to be met by the implementor.

Protection of Invariance, i.e. invalid states are impossible

### CQS (Command Query Seperation)
Method/Function calls can be either a command or a query - not both

```c#
// you can tell a command becuase it returns void
public void Save()
{
    // a command mutates state
    // you can call a query from a command
}

// a query returns data
public string Get()
{
    // and should be idempotent
    // you should not call a command from a query
}
```
 
### Postels Law (Robustess Principle)
How can you trust a command accepts your input?
How can you trust a query to return a useful response

Postels law says "You should be very conservative in what you send, but you should be very liberal in what you accept"
i.e. if you can understand what the client "meant" you should accept their input, but if you cannot you should fail fast, for example null checking.

Another way to deal with if an operation is legal to invoke is to use Options/Maybe

Encapsulation is all about making it easier for client programmers, including your later self, to use the classes you write.
