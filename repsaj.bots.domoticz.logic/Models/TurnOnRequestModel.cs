using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.App.Logic.Models
{
    public class TurnOnRequestModel
    {
        public bool On { get; set; }
        public string Room { get; set; }
        public string Device { get; set; }
    }
}
