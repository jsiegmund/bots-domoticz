using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.App.Logic.Exceptions
{
    public class DomoticzApiException : Exception
    {
        public DomoticzApiException() { }
        public DomoticzApiException(string message) : base(message) { }
        public DomoticzApiException(string message, Exception inner) : base(message, inner) { }
    }
}
