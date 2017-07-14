using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Models
{
    public class DomoticzSettingsModel
    {
        public string BaseUri { get; internal set; }
        public string AccessToken { get; internal set; }

        public DomoticzSettingsModel()
        {

        }

        public DomoticzSettingsModel(string baseUri, string accessToken)
        {
            BaseUri = baseUri;
            AccessToken = accessToken;
        }

        public DomoticzSettingsModel(string baseUri, string user, string password)
        {
            BaseUri = baseUri;
            AccessToken = Helpers.Encoding.Base64Encode($"{user}:{password}");
        }

    }
}
