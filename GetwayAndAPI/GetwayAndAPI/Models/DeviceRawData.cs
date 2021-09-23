using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetwayAndAPI.Models
{
    public class DeviceRawData
    {
        public Guid DeviceGUID { get; set; }
        public DateTime DataCreatedTime { get; set; }
        public decimal[] Data { get; set; }
    }
}
