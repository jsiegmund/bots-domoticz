using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Helpers
{
    public static class RequestUriHelper
    {
        public static readonly string BaseUrl = "domoticz";
        public static readonly string PayloadKey = "payload";
        public static readonly string TypeKey = "type";

        public static Uri ConstructUri(object payload)
        {
            string type = payload.GetType().Name;
            string payloadString = Encoding.Base64Encode(payload);

            return new Uri($"{BaseUrl}:?{TypeKey}={type}&{PayloadKey}={payloadString}");
        }

        public static GenericRequestModel GetModelFromUri(Uri requestUri)
        {
            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(requestUri.Query);

            string type = queryParams[TypeKey].ToString();
            string payloadStr = queryParams[PayloadKey].ToString();

            return new GenericRequestModel()
            {
                Type = type,
                Base64Payload = payloadStr
            };
        }
    }
}
