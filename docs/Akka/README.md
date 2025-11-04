# Akka.net

Example custom logger

```c#
using Akka.Configuration;
using Akka.Dispatch;
using Akka.Hosting.Logging;

// TODO include Actor props like LogSource
/// <summary>
/// This logger calls the underlying Microsoft.Extensions.Logging ILogger in a way that preserves the structured logging.
/// Based off of https://github.com/akkadotnet/Akka.Logger.Serilog/blob/dev/src/Akka.Logger.Serilog/SerilogLogger.cs
/// </summary>
public class CustomOtelLogger : ReceiveActor, IRequiresMessageQueue<ILoggerMessageQueueSemantics>
{
    protected readonly ILoggingAdapter InternalLogger = Akka.Event.Logging.GetLogger(
        Context.System.EventStream,
        nameof(LoggerFactoryLogger));

    private ILogger<ActorSystem> _logger;

    public CustomOtelLogger()
    {
        var setup = Context.System.Settings.Setup.Get<LoggerFactorySetup>();
        if (!setup.HasValue)
            throw new ConfigurationException(
                $"Could not start {nameof(LoggerFactoryLogger)}, the required setup class " +
                $"{nameof(LoggerFactorySetup)} could not be found. Have you added this to the ActorSystem setup?");

        _logger = setup.Value.LoggerFactory.CreateLogger<ActorSystem>();

        Receive<Akka.Event.Error>(Log);
        Receive<Akka.Event.Warning>(Log);
        Receive<Akka.Event.Info>(Log);
        Receive<Akka.Event.Debug>(Log);
        Receive<InitializeLogger>(_ =>
        {
            InternalLogger.Info($"{nameof(CustomOtelLogger)} starting up");
            Sender.Tell(new LoggerInitialized());
        });
    }

    private void Log(Akka.Event.LogEvent logEvent)
    {
        _logger.Log(
            LogLevel(logEvent.LogLevel()),
            new EventId(),
            logEvent.Cause,
            Message(logEvent.Message),
            Args(logEvent.Message));
    }

    private static Microsoft.Extensions.Logging.LogLevel LogLevel(Akka.Event.LogLevel level)
    {
        return level switch
        {
            Akka.Event.LogLevel.DebugLevel => Microsoft.Extensions.Logging.LogLevel.Debug,
            Akka.Event.LogLevel.InfoLevel => Microsoft.Extensions.Logging.LogLevel.Information,
            Akka.Event.LogLevel.WarningLevel => Microsoft.Extensions.Logging.LogLevel.Warning,
            Akka.Event.LogLevel.ErrorLevel => Microsoft.Extensions.Logging.LogLevel.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, "Unknown Akka.Event.LogLevel!")
        };
    }

    private static string? Message(object message) =>
        message is LogMessage logMessage
            ? logMessage.Format
            : message.ToString();

    private static object[] Args(object message) =>
        message is LogMessage logMessage
            ? logMessage.Parameters().ToArray()
            : [];
}

```
