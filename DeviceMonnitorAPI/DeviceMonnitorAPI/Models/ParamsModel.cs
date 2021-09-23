using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class ParamsModel
    {
        public Guid DeviceGUID { get; set; }
        public List<SingleParamModel> Params { get; set; }
    }

}