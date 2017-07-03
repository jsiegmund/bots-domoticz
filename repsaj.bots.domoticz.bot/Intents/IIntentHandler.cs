using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repsaj.Bots.Domoticz.Bot.Intents
{
    public interface IIntentHandler
    {
        Uri HandleTurnOn(LuisResult result);
        Uri HandleTurnOff(LuisResult result);
        Uri HandleScene(LuisResult result);
    }
}