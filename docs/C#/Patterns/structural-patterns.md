# Structural Patterns

## Adapter (a.k.a Wrapper)
Adapte one interface into another to suit some need using inheritance.

- Use the Adapter class when you want to use some existing class, but its interface isn’t compatible with the rest of your code.
- Use the pattern when you want to reuse several existing subclasses that lack some common functionality that can’t be added to the superclass.


## Bridge
Split a large class or a set of closely related classes into two separate hierarchies - abstraction and implementation.

For example instead of having classes; `RedSquare`, `BlueSquare`, `RedCircle`, `BlueCircle`.
Extract one dimension into a seperate class hierarchy and reference this; `Circle(IColor color)`, `Square(IColor color)`

- Use the Bridge pattern when you want to divide and organize a monolithic class that has several variants of some
  functionality (for example, if the class can work with various database servers).
- Use the pattern when you need to extend a class in several orthogonal (independent) dimensions.
- Use the Bridge if you need to be able to switch implementations at runtime.

## Composite (a.k.a ObjectTree)
Compose objects into tree structures and then work with these structures as if they were individual objects.

Create `Leaf` and `Container` classes that share a common interface. The container should provide an array for storing
sub elements. You can interact with composite objects as if they were single. 

- Use the Composite pattern when you have to implement a tree-like object structure.
- Use the pattern when you want the client code to treat both simple and complex elements uniformly.

## Decorator (a.k.a Wrapper (again))
Decorator is a structural design pattern that lets you attach new behaviors to objects by placing these objects inside special wrapper objects that contain the behaviors. It differs from Adapter pattern in that you are not changing the interface you are only proxying the calls, altering the bahaviour either before or after the call. 

- Use the Decorator pattern when you need to be able to assign extra behaviors to objects at runtime without breaking the code that uses these objects.
- Use the pattern when it’s awkward or not possible to extend an object’s behavior using inheritance.