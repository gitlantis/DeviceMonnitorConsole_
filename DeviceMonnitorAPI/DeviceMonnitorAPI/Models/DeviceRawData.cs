using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class ChildData
    {
        public decimal[] AI { get; set; }
        public decimal[] AO { get; set; }
        public decimal[] DI { get; set; }
        public decimal[] DO { get; set; }
        public DateTime Date { get; set; }
        public string[] METADATA { get; set; }
    }

    public class DeviceRawData
    {
        public Guid DeviceGUID { get; set; }
        public DateTime DataCreatedTime { get; set; }
        public string fbHash { get; set; }
        public string guidName { get; set; }
        public ChildData tData { get; set; }        

    }
}
