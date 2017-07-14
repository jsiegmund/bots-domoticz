using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Exceptions
{
    public class SettingsNotInitializedException : Exception
    {
        public SettingsNotInitializedException() { }
        public SettingsNotInitializedException(string message) : base(message) { }
        public SettingsNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }
}
