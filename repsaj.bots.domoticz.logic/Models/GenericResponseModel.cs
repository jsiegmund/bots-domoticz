using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.App.Logic.Models
{
    public class GenericResponseModel
    {
        public string Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
    }
}
