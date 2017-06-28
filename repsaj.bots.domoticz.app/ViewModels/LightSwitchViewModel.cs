using Repsaj.Bots.Domoticz.App.Logic.Helpers;
using Repsaj.Bots.Domoticz.App.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.App.ViewModels
{
    public class LightSwitchViewModel : NotificationBase
    {
        public string DimmerLevels { get; set; }
        public int IDx { get; set; }
        public bool IsDimmer { get; set; }
        public string Name { get; set; }

        public LightSwitchViewModel(LightSwitchModel source) : base()
        {
            this.DimmerLevels = source.DimmerLevels;
            this.IDx = source.IDx;
            this.IsDimmer = source.IsDimmer;
            this.Name = source.Name;
        }
    }
}
