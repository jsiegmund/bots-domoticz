using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.App.Logic.ApiConnector
{
    public static class ApiRequests
    {
        private static Uri BuildUri(string baseUri, IDictionary<string, string> parameters)
        {
            string requestStr = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(baseUri, parameters);
            Uri requestUri = new Uri(requestStr);
            return requestUri;
        }

        internal static Uri GetLightSwitches(string baseUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "command" },
                { "param", "getlightswitches" }
            };

            return BuildUri(baseUri, parameters);
        }

        internal static Uri GetScenes(string baseUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "type", "scenes" }
            };

            return BuildUri(baseUri, parameters);
        }
    }
}
