using System;

namespace LC.Backend.Common.Commands
{
    //Marker interface
    public interface ICommand
    {
        Guid CorrelationId { get; set; }
    }
}