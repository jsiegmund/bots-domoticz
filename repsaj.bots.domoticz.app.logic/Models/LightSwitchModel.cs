using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.App.Logic.Models
{
    public class LightSwitchModel
    {
        public string DimmerLevels { get; set; }
        public bool IsDimmer { get; set; }
        public string Name { get; set; }
        public string SubType { get; set; }
        public string Type { get; set; }
        public int IDx { get; set; }
    }
}
