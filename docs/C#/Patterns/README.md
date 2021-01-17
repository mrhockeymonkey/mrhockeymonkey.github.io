# Patterns

## Builder
Builder pattern is good for building complex objects piece-wise. 
```c#
var stringBuilder = new StringBuilder()
stringBuilder
    .Append("foo")
    .AppendLine("bar")
    .ToString();
```

Pattern Variations

- [SimpleBuilder](./Builder/SimpleBuilder.cs) - A simple example.
- [RecursiveGenericBuilder](./Builder/RecursiveGenericBuilder.cs) - Allows multiple inheritance of a base builder using generics. Use when you want your builder to be extended.
- [FunctionalBuilder](./Builder/FunctionalBuilder.cs) - A functional approach to builder pattern which applies a collection of actions/functions to the object.
- [FunctionalBuilderGeneric](./Builder/FunctionalBuilderGeneric.cs) - Functional builder generic base class. 


## Factory

==foo==

```yaml
--8<-- "foo.yml"
```

```js
--8<-- "config.js"
```
--8<-- "config.js"