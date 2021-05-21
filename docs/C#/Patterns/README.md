# Patterns

## Builder
Builder pattern is ^^good for building complex objects^^ piece-wise. Consider using a builder instead of 
numerous contructor parameters to account for all possible variations of an object. Builders could have a
constructor or returned via a static property. 
```c#
var stringBuilder = new StringBuilder()
stringBuilder
    .Append("foo")
    .AppendLine("bar")
    .ToString();
```


Pattern Variations

- [:octicons-file-code-24: SimpleBuilder][1] - A simple example with a fluent api.
- [:octicons-file-code-24: RecursiveGenericBuilder][2] - Allows inheritance of a base builder using generics. This pattern allows different concrete builders for different variations of an object. It also allows chained inheritance further to exted under the open-closed principle.
- [:octicons-file-code-24: FunctionalBuilder][3] - A functional approach to builder pattern which applies a collection of actions/functions to the object.
- [:octicons-file-code-24: FunctionalBuilderGeneric][4] - Functional builder generic base class. 
- [:octicons-file-code-24: FacetedBuilder][5] - Different facets of an object can be built with different builders via a base class.
- [Director] TODO - You can take the pattern further to collect the steps for different products into a director class. This way you collect all the "recepies" into one class that can make use of builders in the correct manner.

[1]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/SimpleBuilder.cs
[2]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/RecursiveGenericBuilder.cs
[3]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/FunctionalBuilder.cs
[4]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/FunctionalBuilderGeneric.cs
[5]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/FacetedBuilder.cs

## Factory
A component responsible for the wholesale (not piecewise) creation of objects

## Prototype
A partially or fully initilized object that you copy/clone and make use of. 

To clone the object you can either:

- Implement your own deep copy logic or,
- Serialize and deserialize.

## Prototype
A components that is instantiated only once.

## Singleton

