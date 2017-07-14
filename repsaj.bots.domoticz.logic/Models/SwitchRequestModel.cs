using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Models
{
    public class SwitchRequestModel
    {
        public bool On { get; set; }
        public string Room { get; set; }
        public string Device { get; set; }
    }
}
