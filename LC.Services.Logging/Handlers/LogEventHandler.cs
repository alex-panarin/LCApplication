using LC.Backend.Common.Events;
using LC.Backend.Common.Logging;
using LC.Services.Logging.Repositories;
using System;
using System.Threading.Tasks;

namespace LC.Services.Logging.Handlers
{
    public class LogEventHandler : IEventHandler<LogEvent>
    {
        private readonly ILogRepository _repository;

        public LogEventHandler(ILogRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(LogEvent @event)
        {
            await _repository.Log(@event.ToLog());
        }
    }
}
