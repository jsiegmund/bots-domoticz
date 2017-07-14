﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Logging
{
    public sealed class LogEventSource : EventSource
    {
        public static LogEventSource Log = new LogEventSource();

        [Event(1, Level = EventLevel.Verbose)]
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(1, message);
        }

        [Event(2, Level = EventLevel.Informational)]
        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(2, message);
        }

        [Event(3, Level = EventLevel.Warning)]
        public void Warn(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(3, message);
        }

        [Event(4, Level = EventLevel.Error)]
        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(4, message);
        }

        [Event(5, Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(5, message);
        }
    }
}
