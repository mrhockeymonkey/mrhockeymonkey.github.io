// See https://aka.ms/new-console-template for more information

using Construction;

Console.WriteLine("Hello, World!");

// If you have an object, then it's fine
// i.e. you should never be able to construct an object with inconsistent state to begin with

# region Defensive code in constructor (Lightweight)

// ctor is a proper factory and should validate all inputs for correctness
var flatWhite1 = new DefensiveConstructorCoffee("Flat White");

// There is no communication however from the ctor that an exception could be thrown, i.e. unpredictable response
try
{
    var americano1 = new DefensiveConstructorCoffee("");
}
catch (ArgumentException ex)
{
    Console.WriteLine("tried to construct object but argument exception thrown");
}

# endregion


# region Builder for complex objects (Heavyweight)

// if the validation logic for a ctor becomes sufficiently complex it is a good idea to introduce a builder
var builder = new CoffeeBuilder();

builder.SetName("Flat White");

// if/else is less expensive than try/catch
// exceptions are very expensive
if (builder.CanBuild())
{
    var flatWhite2 = builder.Build();
}
else
{
    // TODO do something smarter
    throw new ArgumentException();
}

// personally I dont like the fact that an unguarded object ctor now exists :'( 
var espresso = new SimpleCoffee("Americano");

#endregion