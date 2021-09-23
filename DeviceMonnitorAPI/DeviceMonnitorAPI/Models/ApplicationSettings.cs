
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Models
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get { return "$ubberKey_"; } set { value = "$ubberKey_"; } }
        public string Client_URL { get; set; }
    }
}