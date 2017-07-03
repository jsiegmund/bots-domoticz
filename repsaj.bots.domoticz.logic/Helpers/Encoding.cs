using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.App.Logic.Helpers
{
    public static class Encoding
    {
        /// <summary>
        /// With thanks to https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(object obj)
        {
            string plainText = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// With thanks to https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static T Base64Decode<T>(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            string plainText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(plainText);
        }
    }
}
