using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace softHardwareAdmin.Models
{
    public class DeviceRawData
    {
        public Guid DeviceGUID { get; set; }
        public DateTime DataCreatedTime { get; set; }
        public Dictionary<string, decimal> DeviceOnceDataPortion { get; set; }
    }
}
