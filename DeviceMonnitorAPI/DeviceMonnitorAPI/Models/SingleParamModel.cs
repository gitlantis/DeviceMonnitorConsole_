using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class SingleParamModel
    {
        public string Name { get; set; } //names
        public string NameDomain { get; set; } //AI/AO DI/DO
        public int NameIndex { get; set; } //1,2,3
        public int OrderIndex { get; set; } //1,2,3
    }
}
