using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softHardwareAdmin
{
    class devSettingStructure
    {
        public LiteDB.ObjectId Id { get; set; }     
        public string DeviceName { get; set; }
        public string DeviceGUID { get; set; }
        public int TypeNo { get; set; }
        public int RefreshTime { get; set; }                 
        public bool Enabled { get; set; }


        public devSettingStructure() { }
        public devSettingStructure(string _DeviceName, string _DeviceGUID, int _TypeNo, int _RefreshTime, bool _Enabled) // конструктор с защитой 
        {
            Id = new LiteDB.ObjectId();
            if (_DeviceName == null) DeviceName = "Нет названия"; else DeviceName = _DeviceName;
            if (_DeviceGUID == null) DeviceGUID = "xxxxx-xxxxxx"; else DeviceGUID = _DeviceGUID;

            if ((_TypeNo < 0) | (_TypeNo > 255)) TypeNo = 0; else TypeNo = _TypeNo;
            if ((_RefreshTime < 0) | (_RefreshTime > 360000)) RefreshTime = 0; else RefreshTime = _RefreshTime;

            Enabled = _Enabled;

        }

    }

    class devSettingsWorking
    {
        public static List<devSettingStructure> GlobalDevicesList = new List<devSettingStructure> { };
        public bool LoadListFromLocalDB()  
        {
            try
            {
                using (var dbb = new LiteDB.LiteDatabase(@"localDevSettingsDB.db"))
                {
                    var col = dbb.GetCollection<devSettingStructure>("devices");
                    var result = col.Count();
                    var ans = col.FindAll();
                    GlobalDevicesList.Clear();
                    foreach (var ts in ans) GlobalDevicesList.Add(ts);

                }
                return true;
            }
            catch { };
            return false;
        }

        public bool SaveListToLocalDB() // ну и обратная процедура - после изменения коллекции сохраняет в локальную базу
        {
            try
            {
                using (var db = new LiteDatabase(@"localDevSettingsDB.db"))
                {
                    var col = db.GetCollection<devSettingStructure>("devices");
                    col.DeleteAll();
                    foreach (devSettingStructure ts in GlobalDevicesList) { var res = col.Insert(ts); };
                }
                return true;
            }
            catch { };

            return false;
        }

        public bool LoadListFromFireBase(FirebaseDB fDB, string hubName) // по сути копирует ветку из FireBase в локальную переменную
        {
            FirebaseDB fdb = fDB.NodePath("devices/" + hubName); // Выбираем ветку

            try
            {
                FirebaseResponse getResponse = fdb.Get(); // читаем из FireBase, и если OK, 
                if (getResponse.Success)
                {
                    string ss = getResponse.JSONContent.ToString();

                    List<devSettingStructure> inList = JsonConvert.DeserializeObject<List<devSettingStructure>>(ss); // конвертируем в структуру

                    devSettingsWorking.GlobalDevicesList.Clear(); // и копируем в ОЗУ, не в локальную БД
                    foreach (devSettingStructure ts in inList) devSettingsWorking.GlobalDevicesList.Add(ts);
                }

                return true;     // ну и если все в порядке, вовращаем True
            }
            catch { };

            return false;
        }

        public bool SaveListToFireBase(FirebaseDB fDB, string hubName) // сохраняем содержимое коллекции в FireBase (опасная функция - предусмотреть защиту!)
        {
            try
            {
                if (GlobalDevicesList.Count > 0) foreach (devSettingStructure dss in GlobalDevicesList) dss.Id = LiteDB.ObjectId.NewObjectId(); // Add timestamp here

                string json = JsonConvert.SerializeObject(GlobalDevicesList, Formatting.Indented); // Раскладывает коллекцию на нодлист

                FirebaseDB firebaseDBTeams = fDB.NodePath("devices/"+ hubName);
                FirebaseResponse putResponse = firebaseDBTeams.Put(json); // Слив в FireBase; // с перезаписью всего нода

                return true;
            }
            catch { };

            return false;
        }

        public void refreshDevicesTable(DataGridView dw)
        {
            dw.Rows.Clear();

            foreach (devSettingStructure t in devSettingsWorking.GlobalDevicesList)
            {

                dw.Rows.Add
                    (
                        t.DeviceName,
                        t.DeviceGUID,
                        "TypeName",
                        t.TypeNo,
                        "AI",
                        "AO",
                        "DI",
                        "DO",
                        t.RefreshTime,
                        t.Enabled
                    );
                dw.CurrentCell = dw.Rows[dw.Rows.Count - 1].Cells[0];

                typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == t.TypeNo);
                if (ts != null)
                {
                    dw.CurrentRow.Cells[2].Value = ts.TypeName;
                    dw.CurrentRow.Cells[3].Value = ts.TypeNo;
                    dw.CurrentRow.Cells[4].Value = ts.AI;
                    dw.CurrentRow.Cells[5].Value = ts.AO;
                    dw.CurrentRow.Cells[6].Value = ts.DI;
                    dw.CurrentRow.Cells[7].Value = ts.DO;
                }
            }

        }
        




    }
}
