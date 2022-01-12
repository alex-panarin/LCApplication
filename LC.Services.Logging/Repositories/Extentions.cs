using LC.Backend.Common.Logging;
using LC.Services.Logging.Entities;
using System.Text.Json;

namespace LC.Services.Logging.Repositories
{
    public static class Extentions
    {
        public static LogRow ToLog(this LogEvent @event)
        {
            var logObject = JsonSerializer.Deserialize<LogObject>(@event.Message);
            return new LogRow()
            {
                Data = logObject?.LogResult,
                Method = logObject?.LogMethod,
                Operation = logObject?.LogOperation,
                CorrelationId = $"{logObject?.CorrelationId}",
                State = logObject?.LogState
            };
        }
    }
}
