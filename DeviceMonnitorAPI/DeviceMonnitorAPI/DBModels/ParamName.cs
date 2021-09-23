using Microsoft.AspNetCore.Mvc;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace DeviceMonnitorAPI.DBModels
{
    public class ParamName
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DeviceGuid { get; set; }
        public string Name { get; set; }
        public string NameDomain { get; set; }
        public int NameIndex { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Device> Devices { get; set; }        
    }
}
