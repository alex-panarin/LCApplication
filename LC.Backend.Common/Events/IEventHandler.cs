using System;
using System.Threading.Tasks;

namespace LC.Backend.Common.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
         Task HandleAsync(T @event);
    }
}