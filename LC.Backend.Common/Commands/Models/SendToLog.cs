using System;

namespace LC.Backend.Common.Commands.Models
{
    public class SendToLog : ICommand
    {
        public string Message { get; set; }
        public Guid CorrelationId { get; set; }
        public string LogLevel { get; set; }
    }
}
