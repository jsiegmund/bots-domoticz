using System;
using System.Collections.Generic;
using System.Text;
using Repsaj.Bots.Domoticz.Logic.Models;
using Repsaj.Bots.Domoticz.Logic.Exceptions;

namespace Repsaj.Bots.Domoticz.Logic.Domoticz
{
    public class DomoticzSettingsService : IDomoticzSettingsService
    {
        readonly static string settingsKey_baseUri = "domoticz_baseUri";
        readonly static string settingsKey_accessToken = "domoticz_accessToken";

        DomoticzSettingsModel _settings;

        public DomoticzSettingsService()
        {

        }


        public void FromDictionary(IDictionary<string, object> values)
        {
            string baseUri = "", accessToken = ""; 

            if (values[settingsKey_baseUri] != null)
                baseUri = values[settingsKey_baseUri].ToString();
            if (values[settingsKey_accessToken] != null)
                accessToken = values[settingsKey_accessToken].ToString();

            _settings = new DomoticzSettingsModel(baseUri, accessToken);
        }

        public void ToDictionary(IDictionary<string, object> values)
        {
            if (_settings == null)
                throw new SettingsNotInitializedException();

            if (values.ContainsKey(settingsKey_baseUri))
                values[settingsKey_baseUri] = _settings.BaseUri;
            else
                values.Add(settingsKey_baseUri, _settings.BaseUri);

            if (values.ContainsKey(settingsKey_accessToken))
                values[settingsKey_accessToken] = _settings.AccessToken;
            else
                values.Add(settingsKey_accessToken, _settings.AccessToken);
        }

        public DomoticzSettingsModel GetSettings()
        {
            if (_settings == null)
                throw new SettingsNotInitializedException();

            return _settings;
        }

        public void SetSettings(DomoticzSettingsModel settings)
        {
            this._settings = settings;
        }
    }
}
