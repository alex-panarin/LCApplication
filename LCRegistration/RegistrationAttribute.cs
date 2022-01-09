using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRegistration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegistrationAttribute : Attribute
    {
        public Type RegistrationType { get; set; }
    }
}
