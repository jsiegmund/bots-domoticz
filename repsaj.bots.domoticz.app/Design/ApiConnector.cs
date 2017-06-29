using Repsaj.Bots.Domoticz.Logic.ApiConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repsaj.Bots.Domoticz.App.Logic.Models;

namespace Repsaj.Bots.Domoticz.App.Design
{
    internal class ApiConnector : IApiConnector
    {
        public Task<IEnumerable<LightSwitchModel>> GetLightSwitches()
        {
            var result = new List<LightSwitchModel>();
            return Task.FromResult<IEnumerable<LightSwitchModel>>(result);
        }

        public Task<IEnumerable<SceneModel>> GetScenes()
        {
            var result = new List<SceneModel>();
            return Task.FromResult<IEnumerable<SceneModel>>(result);
        }

        public Task RunCommand(string command)
        {
            return Task.CompletedTask;
        }
    }
}
