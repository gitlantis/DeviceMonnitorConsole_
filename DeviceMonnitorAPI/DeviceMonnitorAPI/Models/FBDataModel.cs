using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class FBDataModel
    {
        public Dictionary<string, Dictionary<DateTime, decimal[]>> Data {get;set;}
    }
}
