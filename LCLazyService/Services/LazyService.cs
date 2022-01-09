using LCRegistration;
using System;
using System.Net.Http;

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
