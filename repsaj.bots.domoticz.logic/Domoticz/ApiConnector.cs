using Repsaj.Bots.Domoticz.Logic.Models;
using Repsaj.Bots.Domoticz.Logic.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Repsaj.Bots.Domoticz.Logic.Domoticz
{
    public class ApiConnector : IApiConnector
    {
        IDomoticzSettingsService _settingsService;

        public ApiConnector(IDomoticzSettingsService settings)
        {
            _settingsService = settings;
        }

        public async Task<IEnumerable<T>> GetRequest<T>(Uri requestUri)
        {
            try
            {
                DomoticzSettingsModel settings = _settingsService.GetSettings();

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", settings.AccessToken);
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

        public async Task<GenericResponseModel> SendRequest(Uri requestUri)
        {
            try
            {
                DomoticzSettingsModel settings = _settingsService.GetSettings();

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", settings.AccessToken);

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
            DomoticzSettingsModel settings = _settingsService.GetSettings();

            string requestStr = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(settings.BaseUri, parameters);
            Uri requestUri = new Uri(requestStr);
            return requestUri;
        }
    }
}
