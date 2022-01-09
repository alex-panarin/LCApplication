using Microsoft.Extensions.DependencyInjection;
using System;

namespace LCRegistration
{
    public interface IRegistration
    {
        void Register(IServiceProvider provider);
    }
}
