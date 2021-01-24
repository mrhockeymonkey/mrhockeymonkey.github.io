# Moq
[:octicons-link-external-24: Github Docs](https://github.com/moq/moq4/wiki/Quickstart)

```c#

mock.Setup(m => m.Name).Returns("Foo"); // property will always return "Foo"

mock.SetupProperty(m => m.Name); // property "tracked" so will update on the mock when set.
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