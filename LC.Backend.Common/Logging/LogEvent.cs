using LC.Backend.Common.Events;
using System;

namespace LC.Backend.Common.Logging
{
    public class LogEvent : IEvent
    {
        public LogEvent(string message, Guid? correlationId)
        {
            Message = message;
            CorrelationId = correlationId;
        }
        protected LogEvent()
        {
        }

        public string Message { get; private set; }
        public Guid? CorrelationId { get; private set; }
    }
}
