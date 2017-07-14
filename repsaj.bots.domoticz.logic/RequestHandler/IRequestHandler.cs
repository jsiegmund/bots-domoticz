using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.Logic.RequestHandler
{
    public interface IRequestHandler
    {
        Task HandleIncomingRequest(string request);
    }
}
