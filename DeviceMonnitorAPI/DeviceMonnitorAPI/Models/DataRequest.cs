using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class DataRequest
    {
        public Guid DeviceGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;

    }
}
