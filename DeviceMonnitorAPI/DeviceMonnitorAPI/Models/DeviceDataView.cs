using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class DeviceDataView
    {
        public string[] ColNames { get; set; }
        public decimal[] ColValues {get;set;}
    }
}
