using Repsaj.Bots.Domoticz.Logic.RequestHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.App.Design
{
    class RequestHandler : IRequestHandler
    {
        public Task HandleIncomingRequest(string request)
        {
            return Task.CompletedTask;
        }
    }
}
