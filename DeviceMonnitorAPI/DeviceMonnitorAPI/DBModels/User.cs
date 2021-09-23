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
    public class User
    {
        [Key]
        public Guid UserGuid { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Index(Unique = true)]
        public string Username { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Token { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime TokenExpire { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime EditedDate { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        //public IList<DeviceUser> DevicesUsers { get; set; }
    }
}
