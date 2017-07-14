using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repsaj.Bots.Domoticz.Logic.Helpers;
using Repsaj.Bots.Domoticz.Logic.Models;
using Repsaj.Bots.Domoticz.Bot.Intents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Tests
{
    [TestClass]
    public class IntentHandlerTests
    {
        IIntentHandler _intentHandler;

        private static string TestName1 = "TestName1";
        private static string TestName2 = "TestName2";
        private static string TestName3 = "TestName3";

        [TestInitialize()]
        public void Initialize()
        {
            _intentHandler = new IntentHandler();
        }

        [TestMethod]
        public void IntentHandler_SwitchOn_Success()
        {
            List<EntityRecommendation> entities = new List<EntityRecommendation>()
            {
                new EntityRecommendation(EntityTypes.HomeAutomation.Device, entity: TestName1),
                new EntityRecommendation(EntityTypes.HomeAutomation.Room, entity: TestName2),
                new EntityRecommendation(EntityTypes.HomeAutomation.Operation, entity: "On")
            };

            LuisResult fakeResult = new LuisResult(TestName3, entities);

            SwitchRequestModel model = _intentHandler.HandleTurnOn(fakeResult);
            Assert.AreEqual(TestName1, model.Device);
            Assert.AreEqual(TestName2, model.Room);
            Assert.AreEqual(true, model.On);
        }

        [TestMethod]
        public void IntentHandler_SwitchOff_Success()
        {
            List<EntityRecommendation> entities = new List<EntityRecommendation>()
            {
                new EntityRecommendation(EntityTypes.HomeAutomation.Device, entity: TestName1),
                new EntityRecommendation(EntityTypes.HomeAutomation.Room, entity: TestName2),
                new EntityRecommendation(EntityTypes.HomeAutomation.Operation, entity: "Off")
            };

            LuisResult fakeResult = new LuisResult(TestName3, entities);

            SwitchRequestModel model = _intentHandler.HandleTurnOff(fakeResult);
            Assert.AreEqual(TestName1, model.Device);
            Assert.AreEqual(TestName2, model.Room);
            Assert.AreEqual(false, model.On);
        }

        [TestMethod]
        public void IntentHandler_Scene_Success()
        {
            List<EntityRecommendation> entities = new List<EntityRecommendation>()
            {
                new EntityRecommendation(EntityTypes.HomeAutomation.Scene, entity: TestName1)
            };

            LuisResult fakeResult = new LuisResult(TestName3, entities);

            SceneRequestModel model = _intentHandler.HandleScene(fakeResult);
            Assert.AreEqual(TestName1, model.SceneName);
        }

        [TestMethod]
        public void IntentHandler_Scene2_Success()
        {
            List<EntityRecommendation> entities = new List<EntityRecommendation>()
            {
                new EntityRecommendation(EntityTypes.HomeAutomation.Scene, entity: "Living room")
            };

            LuisResult fakeResult = new LuisResult(TestName3, entities);

            SceneRequestModel model = _intentHandler.HandleScene(fakeResult);
            Uri requestUri = RequestUriHelper.ConstructUri(model);

            //Assert.AreEqual(TestName1, model.SceneName);
        }

        [TestMethod]
        public void IntentHandler_ConvertUri()
        {
            SwitchRequestModel model = new SwitchRequestModel()
            {
                Device = TestName1,
                Room = TestName2,
                On = true
            };

            Uri requestUri = RequestUriHelper.ConstructUri(model);

            Assert.IsNotNull(requestUri);
            Assert.AreEqual(RequestUriHelper.BaseUrl, requestUri.Scheme);

            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(requestUri.Query);
            Assert.AreEqual(2, queryParams.Count);

            Assert.AreEqual(model.GetType().Name, queryParams["type"].ToString());
            Assert.IsNotNull(queryParams["payload"]);
        }

        [TestMethod]
        public void Encoding_Base64Decode_Success()
        {
            string payload = "eyJPbiI6dHJ1ZSwiUm9vbSI6IlRlc3ROYW1lMiIsIkRldmljZSI6IlRlc3ROYW1lMSJ9";
            SwitchRequestModel model = Logic.Helpers.Encoding.Base64Decode<SwitchRequestModel>(payload);

            Assert.IsNotNull(model);
            Assert.AreEqual(TestName1, model.Device);
            Assert.AreEqual(TestName2, model.Room);
            Assert.AreEqual(true, model.On);            
        }
    }
}
