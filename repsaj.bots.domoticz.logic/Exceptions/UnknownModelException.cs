using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Exceptions
{

    [Serializable]
    public class UnknownModelException : Exception
    {
        public UnknownModelException() { }
        public UnknownModelException(string message) : base(message) { }
        public UnknownModelException(string message, Exception inner) : base(message, inner) { }
    }
}
