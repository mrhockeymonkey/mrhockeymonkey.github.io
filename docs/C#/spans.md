# Spans & Memory

`Span<T>` is a `ref struct` which means it is always allocated on the stack. This is more effecient that using heap
Because it can only be allocated it cannot be used with async methods

For spans that represent immutable or read-only structures, use `ReadOnlySpan<T>`.

## Slicing
This allows you to work with a section on allocated memory without having to first copy it.

```c#
int[] arr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
Span<int> span = arr.AsSpan();

Span<int> firstThree1 = span.Slice(start: 0, length: 3);
Span<int> firstThree2 = span[0..3];
Span<int> firstThree3 = span[new Range(start: 0, end: 3)]; // implicit conversion in Index

Span<int> lastThree1 = span[^3..]; // ^ meaning "from end"
Span<int> lastThree2 = span[Range.StartAt(new Index(3, fromEnd: true))];
```

The same can be done with string but using `ReadOnlySpan<char>` because strings are immutable.
This is better than using `Substring()` when doing extensive string manipulation because it does not
require allocating and copying the original string.

```c#
string name = "Scott Matthews";
ReadOnlySpan<char> nameSpan = name.AsSpan();
ReadOnlySpan<char> nameSpanAlt = "Scott Matthews"; // implicitly converts
// Could refactor to SomeMethod(ReadOnlySpan<char> str)

int lengthOfFirst = name.IndexOf(" ", StringComparison.Ordinal);
int lenghtOfLast = name.Length - lengthOfFirst - 1;

var firstName = nameSpan[0..lengthOfFirst];
var lastName = nameSpan[^lenghtOfLast..];
```

## Use Span<char> instead of string

