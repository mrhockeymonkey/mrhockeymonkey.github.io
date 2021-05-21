# Structural Design Patterns

## Adapter

## Bridge
A mechanism that decouples an interface (hierarchy) from an implementaton (hierarchy). 
i.e. connect different abstractions together instead of building big hierarchys.

- Use the pattern when you need to extend a class in several orthogonal (independent) dimensions.

## Composite


## Decorator (a.k.a. Wrapper)
This structural pattern allows you to extend the behaviour of an object without using inheritance by instead wrapping it in another object.
 
- A decorator keeps a reference to the decorated object(s)
- It may or may not replicate the api of the decorated object
- - You can use R# to generate delegated members
- Use decorators when you need to be able to assign extra behaviors to objects at runtime without breaking the code that uses these objects.
- Use decorators when it’s awkward or not possible to extend an object’s behavior using inheritance
- - For example when a class is marked `sealed` or `final`

Examples:

## Facade
Provides a simple, easy to understand user interface over a large and sophisticated body of code

- Build a facade to provide a simplified API over a set of classes
- You may allow power users to use more complex "lower level" APIs for more control  

## Flyweight

## Proxy