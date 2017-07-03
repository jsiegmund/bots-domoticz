using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.ApiConnector
{
    public static class ApiRequests
    {
        private static Uri BuildUri(string baseUri, IDictionary<string, string> parameters)
        {
            string requestStr = QueryHelpers.AddQueryString(baseUri, parameters);
            Uri requestUri = new Uri(requestStr);
            return requestUri;
        }

        public static Uri GetLightSwitches(string baseUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "getlightswitches" }
            };

            return BuildUri(baseUri, parameters);
        }

        public static Uri GetScenes(string baseUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "scenes" }
            };

            return BuildUri(baseUri, parameters);
        }


        /// <summary>
        /// /json.htm?type=command&param=switchlight&idx=99&switchcmd=On
        /// </summary>
        /// <returns></returns>
        public static Uri Switch(string baseUri, int idx, bool on)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "switchlight" },
                { "idx", idx.ToString() },
                { "switchcmd", on ? "On" : "Off" }
            };

            return BuildUri(baseUri, parameters);
        }

        /// <summary>
        /// /json.htm?type=command&param=switchlight&idx=99&switchcmd=On
        /// </summary>
        /// <returns></returns>
        public static Uri SwitchOn(string baseUri, int idx)
        {
            return Switch(baseUri, idx, true);
        }

        /// <summary>
        /// /json.htm?type=command&param=switchlight&idx=99&switchcmd=Off
        /// </summary>
        /// <returns></returns>
        public static Uri SwitchOff(string baseUri, int idx)
        {
            return Switch(baseUri, idx, false);
        }

        /// <summary>
        /// /json.htm?type=command&param=switchlight&idx=99&switchcmd=Toggle
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static Uri SwitchToggle(string baseUri, int idx)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "switchlight" },
                { "idx", idx.ToString() },
                { "switchcmd", "Toggle" }
            };

            return BuildUri(baseUri, parameters);
        }

        /// <summary>
        /// /json.htm?type=command&param=switchscene&idx=&switchcmd=
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static Uri SwitchScene(string baseUri, int idx)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "switchscene" },
                { "idx", idx.ToString() },
                { "switchcmd", "On" }      // scenes can only be switched on
            };

            return BuildUri(baseUri, parameters);
        }

        /// <summary>
        /// /json.htm?type=command&param=switchscene&idx=&switchcmd=
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="idx"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public static Uri SwitchGroup(string baseUri, int idx, bool on)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "switchscene" },
                { "idx", idx.ToString() },
                { "switchcmd", on ? "On" : "Off" }      // scenes can only be switched on
            };

            return BuildUri(baseUri, parameters);
        }

        /// <summary>
        /// /json.htm?type=command&param=switchlight&idx=99&switchcmd=Set%20Level&level=6
        /// </summary>
        /// <returns></returns>
        public static Uri SetDimmerLevel(string baseUri, int idx, int level)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "switchlight" },
                { "idx", idx.ToString() },
                { "switchcmd", "Set Level" },
                { "level", level.ToString() }
            };

            return BuildUri(baseUri, parameters);
        }
    }
}
