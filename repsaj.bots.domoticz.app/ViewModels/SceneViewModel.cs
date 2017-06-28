using Repsaj.Bots.Domoticz.App.Logic.Helpers;
using Repsaj.Bots.Domoticz.App.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsaj.Bots.Domoticz.App.ViewModels
{
    public class SceneViewModel : NotificationBase
    {
        public int IDx { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public SceneViewModel(SceneModel source)
        {
            this.Name = source.Name;
            this.Status = source.Status;
            this.Type = source.Type;
            this.IDx = source.IDx;
        }
    }
}
