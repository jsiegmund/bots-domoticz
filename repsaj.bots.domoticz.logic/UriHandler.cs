using System;
using System.Collections.Specialized;

namespace Repsaj.Bots.Domoticz.App.Logic
{
    public static class UriHandler
    {
        public static void HandleUri(string uri)
        {
            var values = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri);
        }

    }
}
