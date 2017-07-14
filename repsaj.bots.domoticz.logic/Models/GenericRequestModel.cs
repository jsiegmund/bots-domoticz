using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Models
{
    public class GenericRequestModel
    {
        public string Type { get; set; }
        public string Base64Payload { get; set; }
    }
}
