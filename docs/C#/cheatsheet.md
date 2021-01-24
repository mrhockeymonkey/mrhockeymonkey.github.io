# Cheatsheet

```c#
// declare type as nullable,  <Nullable>enabled</Nullable>
int? length; 

// suppress "not-initialized" warning if needed (eg unit tests without ctor)
string foo = default!;

// null conditional - null if people is null
people?.Length; 
people?[0];

// null forgiving operator - asure compiler people is not null
people!.Length; 
```
