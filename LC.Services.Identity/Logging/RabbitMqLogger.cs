using LC.Backend.Common.Logging;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Logging
{
    public class RabbitMqLogger : ILogger
    {
        private readonly IBusClient _busClient;

        public RabbitMqLogger(IBusClient busClient)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information || logLevel == LogLevel.Error;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            var logEvent = state is LogObject lo 
                ? new LogEvent(lo.ToLogMessage(), lo.CorrelationId)
                : new LogEvent(formatter?.Invoke(state, exception), Guid.NewGuid());
            
            Task.Run(async () => await _busClient.PublishAsync(logEvent));
        }
    }
}
