using Repsaj.Bots.Domoticz.Logic.Domoticz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repsaj.Bots.Domoticz.Logic.Models;

namespace Repsaj.Bots.Domoticz.App.Design
{
    internal class ApiConnector : IApiConnector
    {
        public Task<IEnumerable<LightSwitchModel>> GetLightSwitches()
        {
            var result = new List<LightSwitchModel>();
            return Task.FromResult<IEnumerable<LightSwitchModel>>(result);
        }

        public Task<IEnumerable<T>> GetRequest<T>(Uri requestUri)
        {
            return Task.FromResult(new List<T>().AsEnumerable<T>());
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

        public Task<GenericResponseModel> SendRequest(Uri requestUri)
        {
            GenericResponseModel response = new GenericResponseModel();
            return Task.FromResult(response);
        }
    }
}
