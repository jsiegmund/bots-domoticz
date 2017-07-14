using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Exceptions
{

    [Serializable]
    public class RequestHandlingException : Exception
    {
        public RequestHandlingException() { }
        public RequestHandlingException(string message) : base(message) { }
        public RequestHandlingException(string message, Exception inner) : base(message, inner) { }
    }
}
