using Repsaj.Bots.Domoticz.Logic.Exceptions;
using Repsaj.Bots.Domoticz.Logic.Helpers;
using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.Logic.Domoticz
{
    public class DomoticzManager : IDomoticzManager
    {
        IApiConnector _apiConnector;
        IDomoticzSettingsService _settingsService;

        private Task<IEnumerable<LightSwitchModel>> _lightSwitchModels;
        private Task<IEnumerable<SceneModel>> _sceneModels;

        public DomoticzManager(IApiConnector apiConnector, IDomoticzSettingsService settings)
        {
            _apiConnector = apiConnector;
            _settingsService = settings;
        }

        #region Getting data from Domoticz
        /// <summary>
        /// /json.htm?type=command&param=getlightswitches
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<LightSwitchModel>> GetLightSwitches(bool forceRefresh = false)
        {
            if (forceRefresh || _lightSwitchModels == null || _lightSwitchModels.Status == TaskStatus.Faulted)
            {
                Uri requestUri = ApiRequests.GetLightSwitches(_settingsService.GetSettings().BaseUri);
                _lightSwitchModels = _apiConnector.GetRequest<LightSwitchModel>(requestUri);
            }

            return _lightSwitchModels;
        }

        public Task<IEnumerable<SceneModel>> GetScenes(bool forceRefresh = false)
        {
            if (forceRefresh || _sceneModels == null || _sceneModels.Status == TaskStatus.Faulted)
            {
                Uri requestUri = ApiRequests.GetScenes(_settingsService.GetSettings().BaseUri);
                _sceneModels = _apiConnector.GetRequest<SceneModel>(requestUri);
            }

            return _sceneModels;
        }
        #endregion

        #region Matching the users input with an actual Domoticz object
        public async Task<string> TryFindScene(string needle)
        {
            string[] haystaq = (await GetScenes()).Select(m => m.Name).ToArray();
            return StringMatching.FindClosestMatch(needle, haystaq);
        }

        public async Task<string> TryFindLightSwitch(string needle)
        {
            string[] haystaq = (await GetLightSwitches()).Select(m => m.Name).ToArray();
            return StringMatching.FindClosestMatch(needle, haystaq);
        }
        #endregion

        #region Performing actions
        public async Task SwitchScene(string sceneName)
        {
            SceneModel scene = (await GetScenes()).FirstOrDefault(s => s.Name.Equals(sceneName, StringComparison.CurrentCultureIgnoreCase));

            Uri requestUri = ApiRequests.SwitchScene(_settingsService.GetSettings().BaseUri, scene.IDx);
            var response = await _apiConnector.SendRequest(requestUri);

            if (response.Status != Statics.ResponseOK)
                throw new DomoticzApiException($"Could not toggle scene {sceneName}.");
        }

        public async Task SwitchOn(string switchName)
        {
            LightSwitchModel lightSwitch = (await GetLightSwitches()).FirstOrDefault(s => s.Name.Equals(switchName, StringComparison.CurrentCultureIgnoreCase));

            Uri requestUri = ApiRequests.SwitchOn(_settingsService.GetSettings().BaseUri, lightSwitch.IDx);
            var response = await _apiConnector.SendRequest(requestUri);

            if (response.Status != Statics.ResponseOK)
                throw new DomoticzApiException($"Could not switch on device {switchName}.");
        }

        public async Task SwitchOff(string switchName)
        {
            LightSwitchModel lightSwitch = (await GetLightSwitches()).FirstOrDefault(s => s.Name.Equals(switchName, StringComparison.CurrentCultureIgnoreCase));

            Uri requestUri = ApiRequests.SwitchOff(_settingsService.GetSettings().BaseUri, lightSwitch.IDx);
            var response = await _apiConnector.SendRequest(requestUri);

            if (response.Status != Statics.ResponseOK)
                throw new DomoticzApiException($"Could not switch off device {switchName}.");
        }

        public async Task SetDimLevel(string switchName, int level)
        {
            LightSwitchModel lightSwitch = (await GetLightSwitches()).FirstOrDefault(s => s.Name.Equals(switchName, StringComparison.CurrentCultureIgnoreCase));

            Uri requestUri = ApiRequests.SetDimmerLevel(_settingsService.GetSettings().BaseUri, lightSwitch.IDx, level);
            var response = await _apiConnector.SendRequest(requestUri);

            if (response.Status != Statics.ResponseOK)
                throw new DomoticzApiException($"Could not set dim level on device {switchName}.");
        }
        #endregion
    }
}
