using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Repsaj.Bots.Domoticz.Logic.Models
{
    [DataContract]
    internal class GenericResultModel<T>
    {
        [DataMember(Name = "result")]
        public List<T> Result { get; set; }
    }
}
