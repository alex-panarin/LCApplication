using LCRegistration;
using System;

namespace LCLazyService.Services
{
    [Registration(RegistrationType = typeof(ILasyService))]
    public class LazyService : ILasyService
    {
        public void DoTask()
        {
            Console.WriteLine("Do some task");
        }
    }
}
