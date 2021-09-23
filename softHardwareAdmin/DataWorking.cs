using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softHardwareAdmin
{

    class dataStructure
    {
        public LiteDB.ObjectId Id { get; set; }
        public string DeviceGUID { get; set; }
        public List<UInt16> AI { get; set; }
        public List<UInt16> AO { get; set; }
        public List<UInt16> DI { get; set; }
        public List<UInt16> DO { get; set; }
        public UInt32 status { get; set; }

        public dataStructure() 
        {
            DeviceGUID = "NO GUID DATA";
            AI = new List<UInt16>();
            AO = new List<UInt16>();
            DI = new List<UInt16>();
            DO = new List<UInt16>();


        }
        //public dataStructure(string _DeviceName, string _DeviceGUID, int _TypeNo, int _RefreshTime, bool _Enabled) // конструктор с защитой 
        //{
        //    Id = new LiteDB.ObjectId();
        //    if (_DeviceName == null) DeviceName = "Нет названия"; else DeviceName = _DeviceName;
        //    if (_DeviceGUID == null) DeviceGUID = "xxxxx-xxxxxx"; else DeviceGUID = _DeviceGUID;

        //    if ((_TypeNo < 0) | (_TypeNo > 255)) TypeNo = 0; else TypeNo = _TypeNo;
        //    if ((_RefreshTime < 0) | (_RefreshTime > 360000)) RefreshTime = 0; else RefreshTime = _RefreshTime;

        //    Enabled = _Enabled;

        //}

    }



    class DataWorking
    {

        public bool SaveElementToLocalDB(dataStructure ds) 
        {
            try
            {
                using (var db = new LiteDatabase(@"localDevDataDB.db"))
                {
                    var col = db.GetCollection<dataStructure>("data");
                     var res = col.Insert(ds); 
                }
                return true;
            }
            catch { };

            return false;
        }

        public List<dataStructure> LoadListFromLocalDB(string guid, DateTime DT)
        {
            List<dataStructure> lst = new List<dataStructure>();
            lst.Clear();
            try
            {
                using (var dbb = new LiteDB.LiteDatabase(@"localDevDataDB.db"))
                {
                    var col = dbb.GetCollection<dataStructure>("data");
                    col.EnsureIndex(x => x.DeviceGUID);
                    var ans = col.Find(x =>  x.DeviceGUID.Equals(guid)  );
                    foreach (var ts in ans) if (ts.Id.CreationTime.Date == DT.Date) lst.Add(ts);
                }
            }
            catch { };

            return lst;
        }

        public void UploadLocalDBtoFireBase(string globalEndPointID)
        {
            FirebaseDB fDB = new FirebaseDB("https://mytestproj-1fda7.firebaseio.com/");

            List<dataStructure> lst = new List<dataStructure>();
            lst.Clear();
            try
            {
                using (var dbb = new LiteDB.LiteDatabase(@"localDevDataDB.db"))
                {
                    var col = dbb.GetCollection<dataStructure>("data");
                     var ans = col.FindAll();

                    foreach (dataStructure ds in ans) lst.Add(ds);

                    try
                    {
                        if (lst.Count > 0)
                        {
                            string json = JsonConvert.SerializeObject(lst, Formatting.Indented);
                            FirebaseDB fdb = fDB.NodePath("data/"+ globalEndPointID);
                            FirebaseResponse putResponse = fdb.Put(json); 
                        }

                    }
                    catch { };


                }
            }
            catch { };
        }


    }
}
