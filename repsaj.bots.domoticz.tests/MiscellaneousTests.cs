using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repsaj.Bots.Domoticz.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Tests
{
    [TestClass]
    public class MiscellaneousTests
    {
        static string livingRoom = "Living Room";
        static string bedRoom = "Bed Room";
        static string diner = "Diner";
        static string bathRoom = "Bath Room";
        static string porch = "Porch";
        static string driveWay = "Drive Way";
        static string garden = "Garden";
        static string backYard = "Back Yard";
        static string kitchen = "Kitchen";

        string[] haystaq = new string[]
        {
            livingRoom,
            bedRoom,
            diner,
            bathRoom,
            porch,
            driveWay,
            garden,
            backYard,
            kitchen
        };

        [TestMethod]
        public void StringMatching_FindClosestMatch_Success()
        {
            string needle, result;

            needle = "bedroom";
            result = StringMatching.FindClosestMatch(needle, haystaq);
            Assert.AreEqual(bedRoom, result);

            needle = "living";
            result = StringMatching.FindClosestMatch(needle, haystaq);
            Assert.AreEqual(livingRoom, result);

            needle = "the kitchen";
            result = StringMatching.FindClosestMatch(needle, haystaq);
            Assert.AreEqual(kitchen, result);

            needle = "nothing";
            result = StringMatching.FindClosestMatch(needle, haystaq);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void StringMatching_StripCommonWords_Success()
        {
            string result;

            result = StringMatching.StripCommonWords("the kitchen");
            Assert.AreEqual("kitchen", result);

            result = StringMatching.StripCommonWords("a room");
            Assert.AreEqual("room", result);
        }
    }
}
