using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.Logic.Domoticz
{
    public interface IApiConnector
    {
        Task<IEnumerable<T>> GetRequest<T>(Uri requestUri);
        Task<GenericResponseModel> SendRequest(Uri requestUri);
    }
}
