using Repsaj.Bots.Domoticz.App.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.App.Logic.ApiConnector
{
    public interface IApiConnector
    {
        Task<IEnumerable<LightSwitchModel>> GetLightSwitches();
        Task<IEnumerable<SceneModel>> GetScenes();
        Task RunCommand(string command);
    }
}
