using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.Commands.Models
{
    public class SendToLog : ICommand
    {
        public string Message { get; set; }
        public Guid CorrelationId { get; set; }
        public string LogLevel { get; set; }
    }
}
