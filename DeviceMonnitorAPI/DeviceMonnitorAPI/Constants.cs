using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI
{
    public class Constants
    {
        //#if DEBUG
        //        static public string connectionStrings = "server=127.0.0.1; port=3306; database=_devControlDB01; user=root; password=TOORUSER98WSH09; Persist Security Info=False; Connect Timeout=300";
        //#else
        static public string connectionStrings = "server=127.0.0.1; port=3306; database=_devControlDB01; user=root; password=My$qlSubberKeyS@ltek; Persist Security Info=False; Connect Timeout=300";
        //static public string connectionStrings = "server=127.0.0.1; port=3306; database=_devControlDB01; user=root; password=; Persist Security Info=False; Connect Timeout=300";
        //#endifd
        static public string JWT_SecureKey = "ca721231-b28d-412e-9a3e-9cd6cc6b864d";
        static public string JWT_Issuer = "https://api.leds.uz";
        static public string JWT_Audience = "https://api.leds.uz";

        static public string FirebaseSecret = "CZPfXMxEIxkOhl41yzZREI890Uu2HMnYrOM0JCTB";//"OA4QKAKnS5wwys2hLMz47QIwutkh1x0UYT1Gc8RY",
        static public string FirebaseUrl = "https://testbase-bcc86-default-rtdb.firebaseio.com/";//"https://devconsole-f3fdb-default-rtdb.firebaseio.com/"
        static public string baseTree = "InputData";

    }
}
