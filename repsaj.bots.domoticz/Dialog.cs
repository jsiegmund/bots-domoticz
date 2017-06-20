using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace FunctionsLibraryProject
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
            IMessageActivity reply = context.MakeMessage();
            reply.Text = "This is the text that Cortana displays.";
            reply.Speak = "This is the text that Cortana will say.";
            await context.PostAsync(reply);

            await context.PostAsync($"You have reached the none intent. You said: {result.Query}"); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("HomeAutomation.TurnOn")]
        public async Task HomeAutomationTurnOn(IDialogContext context, LuisResult result)
        {
            IMessageActivity reply = context.MakeMessage();
            reply.Text = "I'm turning on things";
            reply.Speak = "Ok, switching on!";

            var operation = result.Entities.SingleOrDefault(e => e.Type == "HomeAutomation.Operation");
            var device = result.Entities.SingleOrDefault(e => e.Type == "HomeAutomation.Device");
            var room = result.Entities.SingleOrDefault(e => e.Type == "HomeAutomation.Room");

            string uri = String.Format("domo://o={0},d={1},r={2}", operation, device, room);

            var message = context.MakeMessage() as IMessageActivity;
            message.ChannelData = JObject.FromObject(new
            {
                action = new { type = "LaunchUri", uri = uri }
            });
            await context.PostAsync(message);

            await context.PostAsync(reply);
            context.Wait(MessageReceived);
        }

        [LuisIntent("HomeAutomation.TurnOff")]
        public async Task HomeAutomationTurnOff(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"ActivateScene");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Activate Scene")]
        public async Task ActivateSceneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"ActivateScene");
            context.Wait(MessageReceived);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "MyIntent" with the name of your newly created intent in the following handler
        [LuisIntent("MyIntent")]
        public async Task MyIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached the MyIntent intent. You said: {result.Query}"); //
            context.Wait(MessageReceived);
        }
    }
}