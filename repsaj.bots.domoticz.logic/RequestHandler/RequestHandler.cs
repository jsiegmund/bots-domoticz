using Newtonsoft.Json;
using Repsaj.Bots.Domoticz.Logic.Exceptions;
using Repsaj.Bots.Domoticz.Logic.Helpers;
using Repsaj.Bots.Domoticz.Logic.Models;
using Repsaj.Bots.Domoticz.Logic.Domoticz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.Logic.RequestHandler
{
    public class RequestHandler : IRequestHandler
    {
        IDomoticzManager _domoticzManager;

        public RequestHandler(IDomoticzManager domoticzManager)
        {
            _domoticzManager = domoticzManager;
        }

        public async Task HandleIncomingRequest(string requestStr)
        {
            Uri request = new Uri(requestStr);

            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(request.Query);
            var queryParamsStr = queryParams.Select(s => new { param = s.Key, value = s.Value.ToString() })
                                            .ToDictionary(s => s.param, s => s.value);

            GenericRequestModel requestModel = RequestUriHelper.GetModelFromUri(request);

            try
            {
                switch (requestModel.Type)
                {
                    case nameof(SceneRequestModel):
                        await HandleSceneRequest(Helpers.Encoding.Base64Decode<SceneRequestModel>(requestModel.Base64Payload));
                        break;
                    case nameof(SwitchRequestModel):
                        await HandleSwitchRequest(Helpers.Encoding.Base64Decode<SwitchRequestModel>(requestModel.Base64Payload));
                        break;
                    default:
                        throw new UnknownModelException($"Cannot handle incoming request. Unknown request type.");
                }
            } catch (Exception ex)
            {
                throw new RequestHandlingException("There was a problem handling the request.", ex);
            }
        }

        private async Task HandleSceneRequest(SceneRequestModel model)
        {
            string sceneName = await _domoticzManager.TryFindScene(model.SceneName);

            if (!String.IsNullOrEmpty(sceneName))
                await _domoticzManager.SwitchScene(sceneName);            
        }

        private async Task HandleSwitchRequest(SwitchRequestModel model)
        {
            string switchName = await _domoticzManager.TryFindLightSwitch(model.Device);
            
            if (!String.IsNullOrEmpty(switchName))
            {
                if (model.On)
                    await _domoticzManager.SwitchOn(switchName);
                else
                    await _domoticzManager.SwitchOff(switchName);
            }
                
        }
    }
}
