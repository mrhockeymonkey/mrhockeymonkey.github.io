# Cheatsheet

## Stylish
```c#
// target-typed new (mainly for fields/properties)
MyType foo = new("str", "str");
Dictionary<String, String>() myDict = new();

// ctor deconstruction
public MyClass(String p1, String p2) => (_p1, _p2) = (p1, p2)

// pattern matching, s is assigned within if only when true
if (shape is Square s)
    return s.Side * s.Side;
```

## Syntax
```c#
// Verbatim - \t is not a tab
@"C:\tmp" 

// Interpolated
$"{fruit} costs {price:C}" 

// declare type as nullable,  <Nullable>enabled</Nullable>
int? length; 

// suppress "not-initialized" warning if needed (eg unit tests without ctor)
// prefer to use a ctor to ensure initialized
string foo = default!;

// null conditional - null if people is null
people?.Length; 
people?[0];

// null forgiving operator - asure compiler people is not null
people!.Length; 

// null coalescing
var result = auther?.Name ?? "unknown";
return name ?? throw new ArgumentNullExcpetion(nameof(name) "Cannot be null");
var name ??= "dave";

// value comparison for value types and records
// reference comparison for objects
== != 
```

## Access Modifiers
```c#

protected // can only access from this or derived class
internal // can only access from within same assembly

protected internal // protected OR internal
// i.e. public internally, only via inheritance externally

private protected // protected AND internal
// i.e. only via inheritance and only internally. 

sealed // disable further inheritance
// my opinion, use for anything not specifically designed for inheritance in a library
// but not so important in appliction code
```

## Numerics

use int for whole numbers
use double for real numbers that wont be compared to other values
use decimal when accuracy is important

- `half` binary16, doesnt exists in c#
- `float` binary32, 4 bytes
- `double` binary64, 8 bytes, do not use if accuracy is important like financial or missles.
- `decimal` binary128, 16 bytes

```plain
https://ciechanow.ski/exposing-floating-point/

Scientific notation is <sign><significand> x 2^<exponent>
−2343.53125 = -1.0010010011110001 x 2^11 

Binary Representation <sign><exponent><significand>
1 10001010 00100100111100010000000

Note that the exponent 11 is 10001010 (138) due to "biasing"
this is basically ignoring the sign of the exponent and shifting the numbers accordingly

```

```c#
// can be represented in decimal, binary and hex
int decimal = 2_000_000;
int binaryNotation = 0b_0001_1110_1000_0100_1000_0000; 
int hexadecimalNotation = 0x_001E_8480;

//
float f = 1.0f; // or F
double do = 2.0; // or d or D
decimal dec = 2.0m; // or M
 
```


## Tuples
```c#
// named tuples
public (string Name, int Number) GetNamedFruit() 
{ 
    return (Name: "Apples", Number: 5); 
}

// deconstruct
(string myName, int myNumber) = GetNamedFruit();
Console.WriteLine($"{myName} and {myNumber} now declared");

```

## Methods
```c#
public void PassingParameters( int byValue, in int byIn, ref int byRef, out int byOut)
{
    // byValue is default, changes to byValue are scoped and do not affect
    // however if byValue is a reference type changes to its properties are possible

    // in specifies that this parameter is passed by reference but is only read by the called method.
    // in is actually a ref readonly. Generally speaking, there is only one use case where in can be helpful: high performance apps dealing with lots of large readonly structs to save on copying

    // ref specifies that this parameter is passed by reference and may be read or written by the called method. (unscoped)

    // out will create a variable to store result in in the calling scope.
}

// params modifier allows variable number of arguments
public static void UseParams(params int[] list) {}
UseParams(1,2,3,4)
public static void UseParams2(params object[] list) {}
UseParams(1, 2, "three", 4, "five")

```

## Switch Expression
```c#
string output = i switch
{
    int fb when fb % 3 == 0 && fb % 5 == 0 => "fizzbuzz",
    int f when f % 3 == 0 => "fizz",
    int b when b % 5 == 0 => "buzz",
    _ => i.ToString(),
};
```

## Records
```c#
public record Pet {
    public string Name { get; init; }
    public string Animal { get; init; }
}

var myPet = Pet { Name: "kitty", Animal: "Dog" };
var newPet = myPet with {Animal: "Cat"};
```