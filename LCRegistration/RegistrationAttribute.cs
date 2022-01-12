using System;

namespace LCRegistration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegistrationAttribute : Attribute
    {
        public Type RegistrationType { get; set; }
    }
}
