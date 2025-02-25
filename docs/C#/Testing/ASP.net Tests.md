# ASP.NET Tests

## WebapplicationFactory for Integration Tests with TestContainers

Setup containers in `MyApplicationFactory.cs`
```c#
public class MyApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly IContainer _memGraph;

    public GraphServiceApplicationFactory()
    {
        _memGraph = CreateMemGraphContainer();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureTestServices(services =>
        {
            // change bootstrap services here
        });
    }

    private IContainer CreateMemGraphContainer() => new ContainerBuilder()
        .WithImage("memgraph/memgraph-mage:1.18.1-memgraph-2.18.1")
        .WithPortBinding(7687, 7687)
        .WithPortBinding(7444, 7444)
        .WithEnvironment("MEMGRAPH_USER", "admin")
        .WithEnvironment("MEMGRAPH_PASSWORD", "")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(7687))
        .Build();

    public async Task InitializeAsync()
    {
        await _memGraph.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _memGraph.DisposeAsync().AsTask();
    }
}
```

Then use in Xunit class fixture

```c#
public class MyTests : IClassFixture<MyApplicationFactory>
{
    private readonly MyApplicationFactory _factory;

    public BootstrapTests(MyApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Test()
    {
        var driver = _factory.Services.GetRequiredService<IDriver>();
        using var session = driver.Session();
        // etc
    }
```

## Override config sources

https://github.com/dotnet/aspnetcore/issues/37680#issuecomment-1235651426

```c#
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { $"{MemGraphConfiguration.Section}:Uri", "bolt://localhost:7687" },
                { $"{MemGraphConfiguration.Section}:Username", "admin" },
                { $"{MemGraphConfiguration.Section}:Password", "password" },
            })
            .Build();
        builder.UseConfiguration(config);
```

## Background Service
```c#
[Test]
public async Task BackgroundService_ShouldCatchAndLogErrors(){
    _clientMock
        .Setup(m => m.GetData())
        .Throws<Exception>();

    using (var cts = new CancellationTokenSource())
        {
            await Task.WhenAny(
                _sut.StartAsync(cts.Token), 
                Task.Delay(1000, cts.Token));
            cts.Cancel();
        }

    _client.Verify(m => m.GetData(), Times.AtLeast(1));
    _loggerMock.Verify(
        m => m.Log(
            It.Is<LogLevel>(l => l == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((obj, type) => true),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)
        ), 
        Times.AtLeast(1));
}
```

## Setup Controller as SUT
```c#
_sut = _fixture
    .Build<MyController>()
    .OmitAutoProperties()
    .Create();
```

## Controller Endpoint Attributes
```c#
[Test]
public void MyController_UsesCorrectAttributes(){
    var endpointMethod = _sut.GetType().GetMethod("SomeEndpoint");

    Assert.That(endpointMethod, Has.Attribute<HttpGetAttribute>())
    Assert.That(endpointMethod, Has.Attribute<RouteAttribute>().Property("Template").EqualTo("books/{id}"));
}
```

## Controller Endpoint Response
```c#
// Arrange
var books = _fixture.CreateMany<BookDto>().ToList();
_bookRepoMock
    .Setup(m => m.GetBooks())
    .Returns(books);

// Act
var response = await _sut.MyEndpointMethod();

// Assert
_bookRepoMock.Verify(m  => m.GetBooks(), Times.Once);

Assert.That(response, Is.TypeOf<OkObjectResponse>()));
var objResponse = response as OkObjectResponse;

Assert.That(objResponse?.Value, Is.Not.Null);
Assert.That(objResponse!.Value, Is.TypeOf<BookDto[]>());
var booksResult = objResult as BookDto[];

Assert.That(booksResult, Is.Not.Null.And.Not.Empty);
Assert.That(
    booksResult!.Select(b => b.Title),
    Is.EquivalentTo(books.Select(b => b.Title));
);
```
