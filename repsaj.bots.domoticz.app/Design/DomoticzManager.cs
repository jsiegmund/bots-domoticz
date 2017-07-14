using Repsaj.Bots.Domoticz.Logic.Domoticz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repsaj.Bots.Domoticz.Logic.Models;

namespace Repsaj.Bots.Domoticz.App.Design
{
    public class DomoticzManager : IDomoticzManager
    {
        List<LightSwitchModel> _lightSwitchModels = new List<LightSwitchModel>();
        List<SceneModel> _sceneModels = new List<SceneModel>();

        public DomoticzManager()
        {

        }

        public Task SetDimLevel(string switchName, int level)
        {
            throw new NotImplementedException();
        }

        public Task SwitchOff(string switchName)
        {
            throw new NotImplementedException();
        }

        public Task SwitchOn(string switchName)
        {
            throw new NotImplementedException();
        }

        public Task SwitchScene(string sceneName)
        {
            throw new NotImplementedException();
        }

        public Task<string> TryFindLightSwitch(string switchName)
        {
            throw new NotImplementedException();
        }

        public Task<string> TryFindScene(string sceneName)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<LightSwitchModel>> IDomoticzManager.GetLightSwitches(bool forceRefresh)
        {
            return Task.FromResult(_lightSwitchModels.AsEnumerable());
        }

        Task<IEnumerable<SceneModel>> IDomoticzManager.GetScenes(bool forceRefresh)
        {
            return Task.FromResult(_sceneModels.AsEnumerable());
        }
    }
}
