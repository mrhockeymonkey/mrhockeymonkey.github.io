# Auto Fixture
Makes setting up tests faster and less prone to breaking when refactoring.

The way AutoFixture works is be looking for public constructors and writable properties and creating
these objects with generated values. 

```c#
[TestFixture]
public class SomeTests 
{
    private IFixture _fixture;

    [SetUp]
    public void BeforeEachTest()
    {
        _fixture = new Fixture().Customize()

        var generatedString = _fixture.Create<string>(); // generate a string
        var generatedString = _fixture.Create<string>("Name"); // seed generated string with a hint to its use
        var generatedClass = _fixture.Create<MyClass>(); // uses public constructor to generate

        var generatedEnumerable = _fixture.CreateMany<T>(); // defaults to 3 but can override with int for desired number
        _fixture.RepeatCount = 5 // override default for many

        var myList = new List<T>();
        _fixture.AddManyTo(myList);

        // set a particular property
        var mc = fixture.Build<MyClass>()
                .With(x => x.MyText, "Ploeh")
                //.Without(p => p.Spouse)
                .Create();

        // disable 
        var sut = fixture.Build<Vehicle>()
                 .OmitAutoProperties()
                 .Create();
    }
}
```

## Register, Inject or Freeze?
[:fontawesome-brands-stack-overflow: Stack Overflow: cant-grasp-the-difference-between-freeze-inject-register](https://stackoverflow.com/questions/18161127/cant-grasp-the-difference-between-freeze-inject-register)

tl;dr Register is considered legacy. Inject and Freeze replace it. 

```c#
// Register
// Informs AutoMock how to create objects that dont have public constructors, like interfaces
// Note: in this case AutoMock (see below) would be a better approach
public MyClass(IMyInterface mi) {}

fixture.Register<IMyInterface>(() => new FakeMyInterface());
var generatedClass = _fixture.Create<MyClass>(); 

_fixture.Register<int, string, IMyInterface>((i, s) => new FakeInterface(i, s));

// Inject 
// simply wraps register like so
public static void Inject<T>(this IFixture, T item){
    if (fixture == null)
        throw new ArgumentException(nameof(fixture));
    fixture.Register<T>(Func<T>(() => item));
}

// so to setup dependencies becomes
var foo = _fixture.Create<Foo>();
_fixture.Inject(foo);

// Freeze
// will create and inject a object in one line
var foo = _fixture.Freeze<Foo>();

```

## Auto-Mocking with Moq
Install `AutoFixture.AutoMoqNuGet` package to enable AutoFixture to create mocks of interfaces using AutoMoq. 
AutoFixture will try to create an object in the normal way and fallback to Moq when required. 

```c#
_fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});

// without automoq this would fail because autofixture would not be able to create IClient
var _client = _fixture.Freeze<IClient>();

// if you want to create and use a mock to verify calls you can explicitly 
var _clientMock = _fixture.Freeze<Mock<IClient>>(); // this tells freeze to return the Mock instead of the Mock.Object
_clientMock.Setup(...);

```