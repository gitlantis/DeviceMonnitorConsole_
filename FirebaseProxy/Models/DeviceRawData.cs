using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirebaseProxy.Models
{
    public class DeviceRawData
    {
        public string DevGUID { get; set; }
        public DateTime DataCreatedTime { get; set; }
        public Dictionary<string, string> DeviceOnceDataPortion { get; set; }
    }
}
