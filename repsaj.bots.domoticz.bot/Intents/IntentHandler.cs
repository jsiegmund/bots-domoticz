using Microsoft.Bot.Builder.Luis.Models;
using Repsaj.Bots.Domoticz.Logic.Helpers;
using Repsaj.Bots.Domoticz.Logic.Models;
using Repsaj.Bots.Domoticz.Logic.Domoticz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repsaj.Bots.Domoticz.Bot.Intents
{
    public class IntentHandler : IIntentHandler
    {
        string _baseUrl = "domoticz:";

        public SceneRequestModel HandleScene(LuisResult luisResult)
        {
            var scene = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Scene);

            SceneRequestModel model = new SceneRequestModel();

            if (scene != null)
                model.SceneName = scene.Entity;

            return model;
        }

        public SwitchRequestModel HandleTurnOff(LuisResult luisResult)
        {
            var operation = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Operation);
            var device = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Device);
            var room = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Room);

            SwitchRequestModel model = new SwitchRequestModel();

            model.On = false;

            if (room != null)
                model.Room = room.Entity;
            if (device != null)
                model.Device = device.Entity;

            return model;
        }

        public SwitchRequestModel HandleTurnOn(LuisResult luisResult)
        {
            var operation = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Operation);
            var device = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Device);
            var room = luisResult.Entities.SingleOrDefault(e => e.Type == EntityTypes.HomeAutomation.Room);

            SwitchRequestModel model = new SwitchRequestModel();

            model.On = true;

            if (room != null)
                model.Room = room.Entity;
            if (device != null)
                model.Device = device.Entity;

            return model;
        }

        internal bool TranslateOnToBool(string input)
        {
            return String.Equals("on", input, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}