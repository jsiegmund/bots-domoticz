using Repsaj.Bots.Domoticz.App.Logic.Models;
using Repsaj.Bots.Domoticz.App.Logic.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Repsaj.Bots.Domoticz.App.Logic.ApiConnector
{
    public class ApiConnector : IApiConnector
    {
        string _baseUri = "<<URL>>";
        string _passwordString = "<<USER:PASS_BASE64>>";

        public ApiConnector()
        {
        }

        /// <summary>
        /// /json.htm?type=command&param=getlightswitches
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<LightSwitchModel>> GetLightSwitches()
        {
            Uri requestUri = ApiRequests.GetLightSwitches(_baseUri);
            return GetRequest<LightSwitchModel>(requestUri);
        }

        public Task<IEnumerable<SceneModel>> GetScenes()
        {
            Uri requestUri = ApiRequests.GetScenes(_baseUri);
            return GetRequest<SceneModel>(requestUri);
        }

        private async Task<IEnumerable<T>> GetRequest<T>(Uri requestUri)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _passwordString);
                HttpResponseMessage response = await client.GetAsync(requestUri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<GenericResultModel<T>>(jsonString);
                    return model.Result;
                }
                else
                {
                    throw new DomoticzApiException($"Request to the Domoticz API failed. Status Code: {response.StatusCode} Message returned: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new DomoticzApiException($"Request to the Domoticz API failed. Check the inner exception for more detail.", ex);
            }
        }

        private async Task<GenericResponseModel> SendRequest(Uri requestUri)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _passwordString);

                HttpResponseMessage response = await client.GetAsync(requestUri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<GenericResponseModel>(jsonString);
                    return model;
                }
                else
                {
                    throw new DomoticzApiException($"Request to the Domoticz API failed. Status Code: {response.StatusCode} Message returned: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new DomoticzApiException($"Request to the Domoticz API failed. Check the inner exception for more detail.", ex);
            }
        }

        private Uri BuildUri(IDictionary<string, string> parameters)
        {
            string requestStr = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(_baseUri, parameters);
            Uri requestUri = new Uri(requestStr);
            return requestUri;
        }

        public async Task RunCommand(string command)
        {
            Uri commandUri = new Uri(command);
            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(commandUri.Query);
            var queryParamsStr = queryParams.Select(s => new { param = s.Key, value = s.Value.ToString() })
                                            .ToDictionary(s => s.param, s => s.value);

            string requestStr = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(_baseUri, queryParamsStr);
            Uri requestUri = new Uri(requestStr);

            var response = await SendRequest(requestUri);

            if (response.Status != "OK")
                throw new DomoticzApiException($"The Domoticz API returned an error: {response.Title}");
        }
    }
}
