using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using Repsaj.Bots.Domoticz.Bot.Intents;
using Repsaj.Bots.Domoticz.Logic.Models;
using Repsaj.Bots.Domoticz.Logic.Helpers;

namespace Repsaj.Bots.Domoticz.Bot
{
    [Serializable]
    public class Dialog : LuisDialog<object>
    {
        public Dialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
        {

        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            string replyText = "I didn't understand that. You might want to rephrase.";

            IMessageActivity reply = context.MakeMessage();
            reply.Text = replyText;
            reply.Speak = replyText;

            await context.PostAsync(reply);
            context.Wait(MessageReceived);
        }

        [LuisIntent("HomeAutomation.TurnOn")]
        public async Task HomeAutomationTurnOn(IDialogContext context, LuisResult result)
        {
            IIntentHandler intentHandler = new Intents.IntentHandler();     // TODO: replace by dependency injection
            SwitchRequestModel model = intentHandler.HandleTurnOn(result);
            Uri gatewayUri = RequestUriHelper.ConstructUri(model);

            var message = context.MakeMessage() as IMessageActivity;
            message.ChannelData = JObject.FromObject(new
            {
                action = new { type = "LaunchUri", uri = gatewayUri.ToString() }
            });

            message.Text = $"Switching on {model.Device}";
            message.Speak = "OK!";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("HomeAutomation.TurnOff")]
        public async Task HomeAutomationTurnOff(IDialogContext context, LuisResult result)
        {
            IIntentHandler intentHandler = new Intents.IntentHandler();     // TODO: replace by dependency injection
            SwitchRequestModel model = intentHandler.HandleTurnOn(result);
            Uri gatewayUri = RequestUriHelper.ConstructUri(model);

            var message = context.MakeMessage() as IMessageActivity;
            message.ChannelData = JObject.FromObject(new
            {
                action = new { type = "LaunchUri", uri = gatewayUri.ToString() }
            });

            message.Text = $"Switching off {model.Device}";
            message.Speak = "OK!";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("HomeAutomation.ActivateScene")]
        public async Task ActivateSceneIntent(IDialogContext context, LuisResult result)
        {
            IIntentHandler intentHandler = new Intents.IntentHandler();     // TODO: replace by dependency injection
            SceneRequestModel model = intentHandler.HandleScene(result);
            Uri gatewayUri = RequestUriHelper.ConstructUri(model);

            var message = context.MakeMessage() as IMessageActivity;
            message.ChannelData = JObject.FromObject(new
            {
                action = new { type = "LaunchUri", uri = gatewayUri.ToString() }
            });

            message.Text = $"Switching scene {model.SceneName}";
            message.Speak = "OK!";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }
    }
}