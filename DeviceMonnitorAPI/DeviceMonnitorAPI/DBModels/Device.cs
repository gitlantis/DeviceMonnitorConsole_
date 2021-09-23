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
    public class Device
    {
        [Key]
        public Guid DeviceGuid { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<DeviceData> DevicesData { get; set; }
        public virtual ICollection<DeviceConfig> DevicesConfig { get; set; }
        public virtual ICollection<ParamName> ParamNames { get; set; }
        //public IList<DeviceUser> DevicesUsers { get; set; }
    }
}
