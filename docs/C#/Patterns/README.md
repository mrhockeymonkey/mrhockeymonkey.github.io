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

- [:octicons-file-code-24: SimpleBuilder][1] - A simple example.
- [:octicons-file-code-24: RecursiveGenericBuilder][2] - Allows multiple inheritance of a base builder using generics. Use when you want your builder to be extended.
- [:octicons-file-code-24: FunctionalBuilder][3] - A functional approach to builder pattern which applies a collection of actions/functions to the object.
- [:octicons-file-code-24: FunctionalBuilderGeneric][4] - Functional builder generic base class. 

[1]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/SimpleBuilder.cs
[2]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/RecursiveGenericBuilder.cs
[3]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/FunctionalBuilder.cs
[4]: https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/blob/master/docs/C%23/Patterns/Builder/FunctionalBuilderGeneric.cs

## Factory

==foo==

```yaml
--8<-- "foo.yml"
```

```js
--8<-- "config.js"
```
--8<-- "config.js"