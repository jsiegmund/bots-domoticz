using Microsoft.Bot.Builder.Luis.Models;
using Repsaj.Bots.Domoticz.App.Logic.Helpers;
using Repsaj.Bots.Domoticz.App.Logic.Models;
using Repsaj.Bots.Domoticz.Logic.ApiConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repsaj.Bots.Domoticz.Bot.Intents
{
    public class IntentHandler : IIntentHandler
    {
        string _baseUrl = "domoticz:";

        public Uri HandleScene(LuisResult result)
        {
            throw new NotImplementedException();
        }

        public Uri HandleTurnOff(LuisResult result)
        {
            throw new NotImplementedException();
        }

        public Uri HandleTurnOn(LuisResult luisResult)
        {
            var operation = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Operation);
            var device = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Device);
            var room = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Room);

            TurnOnRequestModel model = new TurnOnRequestModel();

            model.On = true;

            if (room != null)
                model.Room = room.Entity;
            if (device != null)
                model.Device = device.Entity;

            return ConstructUri(GatewayOperations.TurnOn, model);
        }

        internal bool TranslateOnToBool(string input)
        {
            return String.Equals("on", input, StringComparison.CurrentCultureIgnoreCase);
        }

        internal Uri ConstructUri(string mode, object payload)
        {
            string payloadString = Encoding.Base64Encode(payload);
            return new Uri($"?mode={mode}&payload={_baseUrl}");
        }
    }
}