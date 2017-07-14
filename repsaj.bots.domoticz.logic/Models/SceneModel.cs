using System;
using System.Collections.Generic;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Models
{
    public class SceneModel
    {
        public bool Favorite { get; set; }
        public int HardwareID { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool Timers { get; set; }
        public string Type { get; set; }
        public int IDx { get; set; }
    }
}
