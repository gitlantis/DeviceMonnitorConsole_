using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class DeviceDataModel
    {
        public Guid Id { get; set; }
        public Guid DeviceGuid { get; set; }
        public Guid DataGuid { get; set; }
        public string DictKey { get; set; }
        public decimal DictValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
