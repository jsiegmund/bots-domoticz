using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Domoticz
{
    public interface IDomoticzSettingsService
    {
        DomoticzSettingsModel GetSettings();
        void SetSettings(DomoticzSettingsModel settings);

        void FromDictionary(IDictionary<string, object> values);
        void ToDictionary(IDictionary<string, object> values);
    }
}
