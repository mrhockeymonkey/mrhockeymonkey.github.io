# NUnit Assertions
NUnit provides the framework for writing tests. An alternative to NUnit is XUnit

[Docs :octicons-link-external-24:](https://docs.nunit.org/articles/nunit/writing-tests/constraints/Constraints.html){_target=blank}

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

## Basics

```c#
// classic, easy to read
Assert.AreEqual(1, 1) 

// constraint, more powerful
Assert.That(array, Has.Exactly(1).EqualTo(obj))
```

Prefer to use contraints for consistency. 
`Is` and `Has` are helper classes to create constraints

```c#
// null or emptiness tests
Assert.That(obj, Is.Not.Null.And.Not.Empty);
Assert.That(list, Is.Empty);

// type tests
Assert.That(obj, Is.TypeOf<T>()); // check type exactly, i.e. runtime type
Assert.That(obj,  Is.InstanceOf<IEnumerable<T>>()); // check obj is type or derived from type

Assert.That(obj, Has.Property("Version"));
Assert.That(array, Is.EquivalentTo(array)); // test that two IEnumerables have same elements in any order
Assert.That(array, Has.Exactly(3).Items); // test size of IEnumerable
Assert.That(array, Has.None.EqualTo("foo"))
Assert.That(array, Is.Unique) // no duplicates
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
Assert.That(async () => await SomeMethodAsync(), Throws.Exception);
```