using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWaySIM800
{

    public class tempData
    {
        //public List<decimal> AI;
        //public List<decimal> AO;
        //public List<decimal> DI;
        //public List<decimal> DO;

        public decimal[] AI { get; set; }
        public decimal[] AO { get; set; }
        public decimal[] DI { get; set; }
        public decimal[] DO { get; set; }


        public DateTime Date { get; set; }
        public string[] METADATA { get; set; }

        public tempData()
        {

        }
        public tempData(tempData td)
        {
            this.AI = new decimal[td.AI.Length]; for (int i = 0; i < td.AI.Length; i++) { this.AI[i] = td.AI[i]; }
            this.AO = new decimal[td.AO.Length]; for (int i = 0; i < td.AO.Length; i++) { this.AO[i] = td.AO[i]; }
            this.DI = new decimal[td.DI.Length]; for (int i = 0; i < td.DI.Length; i++) { this.DI[i] = td.DI[i]; }
            this.DO = new decimal[td.DO.Length]; for (int i = 0; i < td.DO.Length; i++) { this.DO[i] = td.DO[i]; }
             this.METADATA= new string[td.METADATA.Length]; 
              for (int i = 0; i < td.METADATA.Length; i++) { this.METADATA[i] = td.METADATA[i]; }
           
            this.Date = td.Date; //?
                
        }


    }
    public class DeviceRawData
    {
        public Guid DeviceGUID { get; set; }
        public DateTime DataCreatedTime { get; set; }
        public string fbHash { get; set; }
        public string guidName { get; set; }
        public tempData tData {get;set;}
        public DeviceRawData()
        { 

        }

        public DeviceRawData(string guidName, Guid DeviceGUID, DateTime DataCreatedTime, tempData Data, string fbHash)
        {
            this.guidName = guidName;
            this.DeviceGUID = DeviceGUID;
            this.DataCreatedTime = DataCreatedTime;
            this.tData = new tempData(Data);
            this.fbHash = fbHash;
        }

    }
}
