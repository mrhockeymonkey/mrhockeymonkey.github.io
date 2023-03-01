# ASP.NET Tests

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
