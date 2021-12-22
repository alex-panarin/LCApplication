using System;

namespace LC.Backend.Common.Operations
{
    public class LCException : Exception
    {
        public LCException(string message)
            : base(message)
        {
            
        }

    }
}
