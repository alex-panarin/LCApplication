using System;
using System.Threading.Tasks;

namespace LC.Backend.Common.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}