# Moq
[:octicons-link-external-24: Github Docs](https://github.com/moq/moq4/wiki/Quickstart)

```c#

mock.Setup(m => m.Name).Returns("Foo"); // property will always return "Foo"

mock.SetupProperty(m => m.Name); // property "tracked" so will update on the mock when set.
```

## Setup Mock for IMemoryCache
```c#
// when testing _someRepository retrieves data once and caches it for subsequent calls.
var cacheKey = "some-key";
var cachedData = _fixture.CreateMany<SomeClass>().ToList();
_someRepository.Setup(m => m.GetStuff()).Returns(cachedData);

_cacheMock.SetupCacheMiss(cacheKey);
var result1 = await _sut.DoSomething();

_cacheMock.SetupCacheHit(cacheKey, cachedData);
var result2 = await _sut.MapAll();

_someRepository.Verify(m => m.GetStuff(), Times.Once);
```

```c#
public static class MemoryCacheExtensions
{
    public static void SetupCacheMiss(this Mock<IMemoryCache> mock, object key)
    {
        object nothing;
        mock
            .Setup(c => c.TryGetValue(key, out nothing))
            .Returns(false);
    }

    public static void SetupCacheHit(this Mock<IMemoryCache> mock, object key, object obj) => 
        mock
            .Setup(c => c.TryGetValue(key, out obj))
            .Returns(true);
}
```

## Mocking ILogger<T>
In some cases it may be useful to verify logs are emitted.

```c#
_loggerMock.Verify(
    m => m.Log(
        It.Is<LogLevel>(l => l == LogLevel.Error),
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((obj, type) => true),
        It.IsAny<Exception>(),
        It.Is<Func<It.IsAnyType, Exception, string>>((obj, type) => true)
    ), 
    Times.AtLeast(1)
);
```
