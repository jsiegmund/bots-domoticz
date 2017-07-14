using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.Logic.Domoticz
{
    public interface IDomoticzManager
    {
        Task<IEnumerable<LightSwitchModel>> GetLightSwitches(bool forceRefresh = false);
        Task<IEnumerable<SceneModel>> GetScenes(bool forceRefresh = false);

        Task<string> TryFindScene(string sceneName);
        Task<string> TryFindLightSwitch(string switchName);

        Task SwitchScene(string sceneName);
        Task SwitchOn(string switchName);
        Task SwitchOff(string switchName);
        Task SetDimLevel (string switchName, int level);
    }
}
