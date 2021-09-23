using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softHardwareAdmin.Models
{
    public class OrgUser
    {        
        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }                     
        public string Role { get; set; }
        public bool IsActive { get; set; }        
        public string Description { get; set; }        
        public DateTime CreatedDate { get; set; }        
        public DateTime EditedDate{get;set;}        
    }
}
