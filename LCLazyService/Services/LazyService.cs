using LCRegistration;
using System;

namespace LCLazyService.Services
{
    public class LazyService : ILasyService, IRegistration
    {
        public void DoTask()
        {
            Console.WriteLine("Do some task");
        }

        public void Register(IServiceProvider provider)
        {
            provider.AddService(typeof(ILasyService), this);
        }
    }
}
