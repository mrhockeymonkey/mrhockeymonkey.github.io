# NUnit Assertions
NUnit provides the framework for writing tests. An alternative to NUnit is XUnit

```c#
[TestFixture]
public class SomeClassTests {
    [SetUp]
    public void BeforeEachTest(){
        // setup 
    }

    [Test]
    public void SomeMethod_DoesSomething_WhenSomeCondition(){
        // test
    }

    [TearDown]
    public void AfterEachTest(){
        // dispose
    }
}
```

There are two "types" of asserts, classic and constraints. 

[Classic]()
[Constraints](https://docs.nunit.org/articles/nunit/writing-tests/constraints/Constraints.html){_target=blank}

```c#
Assert.AreEqual(1, 1) // classic, easy to read
Assert.That(array, Has.Exactly(1).EqualTo(obj)) // constraint, more powerful
```

Prefer to use contraints for consistency. `It` and `Has` are helper classes to create constraints

## Basics
```c#
Assert.That(obj, It.Not.Null.And.Not.Empty) // 
Assert.That(obj, Has.Property("Version"));

Assert.That(obj, Is.TypeOf<T>(typeof(T))) // check type exactly, i.e. runtime type
Assert.IsInstanceOf<IEnumerable<T>>(obj) // check obj is type or derived from type

Assert.That(array, Is.EquivalentTo(array)); // test that two IEnumerables have same elements in any order
Assert.That(array, Has.Exactly(3).Items); // test size of IEnumerable
Assert.That(array, Has.None.EqualTo("foo"))
Assert.That(array, Is.Unique)
Assert.That(new int[] { 1, 2, 3 }, Has.Exactly(1).EqualTo(1).And.Exactly(1).EqualTo(3)); // <Constraint>.And.<Constraint>

Assert.That(
    array.Select(e => e.SomeProperty),
    Has.All.EqualTo("foo")); // check a property of all element in an array.
```

## Check Method Attributes
```c#
var someMethod = _sut.GetType().GetMethod("SomeMethod");
Assert.That(someMethod, Has.Attribute<HttpGetAttribute>()); // test an attribute exists
Assert.That(someMethod, Has.Attribute<RouteAttribute>().Property("Template").EqualTo("books/{id}")); // test an attribute has desired property
```

## Reusable Contraints
```c#
// constraint must be declared as reusable to work as intended
ReusableConstraint myConstraint = Is.Not.Null;
Assert.That("not a null", myConstraint); 
Assert.That("not a null", myConstraint);
```

## Test Methods Throws Exception
```c#
Assert.That(SomeMethod, Throws.TypeOf<ArgumentException>());
Assert.That(() => { throw new ArgumentException(); }, Throws.ArgumentException);
Assert.That(() => SomeMethod(actual), Throws.Nothing);
.Has.Message("My Error message")???
```