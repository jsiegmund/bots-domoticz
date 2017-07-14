using Microsoft.Bot.Builder.Luis.Models;
using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repsaj.Bots.Domoticz.Bot.Intents
{
    public interface IIntentHandler
    {
        SwitchRequestModel HandleTurnOn(LuisResult result);
        SwitchRequestModel HandleTurnOff(LuisResult result);
        SceneRequestModel HandleScene(LuisResult result);
    }
}