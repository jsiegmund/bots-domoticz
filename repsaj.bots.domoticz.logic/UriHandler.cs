using System;
using System.Collections.Specialized;

namespace Repsaj.Bots.Domoticz.Logic
{
    public static class UriHandler
    {
        public static void HandleUri(string uri)
        {
            var values = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri);
        }

    }
}
