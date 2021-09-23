using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWaySIM800
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public DateTime? expires { get; set; }
    }
}
