using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repsaj.Bots.Domoticz.Logic.Domoticz;
using System;

namespace Repsaj.Bots.Domoticz.Tests
{
    [TestClass]
    public class ApiRequestTests
    {
        string _baseUrl = "https://domoticz/json.htm";
            
        [TestMethod]
        public void Api_GetLightsSwitches_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=getlightswitches");
            Uri result = ApiRequests.GetLightSwitches(_baseUrl);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_GetScenes_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=scenes");
            Uri result = ApiRequests.GetScenes(_baseUrl);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_Switch_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchlight&idx=99&switchcmd=On");
            Uri result = ApiRequests.Switch(_baseUrl, 99, true);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_SwitchOn_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchlight&idx=99&switchcmd=On");
            Uri result = ApiRequests.SwitchOn(_baseUrl, 99);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_SwitchOff_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchlight&idx=99&switchcmd=Off");
            Uri result = ApiRequests.SwitchOff(_baseUrl, 99);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_SwitchToggle_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchlight&idx=99&switchcmd=Toggle");
            Uri result = ApiRequests.SwitchToggle(_baseUrl, 99);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_SetDimmerLevel_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchlight&idx=99&switchcmd=Set%20Level&level=6");
            Uri result = ApiRequests.SetDimmerLevel(_baseUrl, 99, 6);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_SwitchScene_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchscene&idx=99&switchcmd=On");
            Uri result = ApiRequests.SwitchScene(_baseUrl, 99);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Api_SwitchGroup_Success()
        {
            Uri expected = new Uri(_baseUrl + "?type=command&param=switchscene&idx=99&switchcmd=Off");
            Uri result = ApiRequests.SwitchGroup(_baseUrl, 99, false);

            Assert.AreEqual(expected, result);
        }
    }
}
