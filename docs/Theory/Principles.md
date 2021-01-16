# Principles

## S.O.L.I.D.
- **Single Responsibility**: A class should only have a single responsibility and thus on one reason to change.
- **Open-Closed Principle**: Entities should be open for extension but closed for modification.
- **Liskov Substitution**: Objects should be replaceable by their subtypes without altering the correctness of the program
- **Interface Segregation Principle**: Many smaller, specific interfaces are better than one general purpose one
- **Dependency Inversion Principle**: High-level modules should not depend on low-level modules. Both should depend on abstractions (interfaces), Abstractions should not depend on details. Details (concrete implementations) should depend on abstractions.

### Notes on Dependency Inversion Principle
One approach when applying the dependency inversion principle the higher lever code should "own" the interface, not the lower level module. This is where the "inversion" comes in. This architecture groups the higher/policy components and the abstractions that define lower services together in the same package. Upper layers can then use other implementations of the lower services.

Another approach when the lower-level layer components are closed or when the application requires the reuse of existing services, it is common that an Adapter mediates between the services and the abstractions.