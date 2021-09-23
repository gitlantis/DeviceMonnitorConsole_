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

    // класс для настройки количества входов/выходов в устройстве
    // to Do - добавить валидацию параметров
   class typeStructure    
    {
        public LiteDB.ObjectId Id { get; set; }     // автоматический индекс для liteDB
        public string TypeName { get; set; }        // название типа устройства
        public int TypeNo { get; set; }             // номер типа устройства
        public int AI { get; set; }                 // количество аналоговых входов
        public int AO { get; set; }                 // количество аналоговых выходов
        public int DI { get; set; }                 // количество цифровых входов
        public int DO { get; set; }                 // количество цифровых выходов

        public typeStructure() { }
        public typeStructure(string tname, int TNo, int ai, int ao, int di, int d0) // конструктор с защитой 
        {
            if (tname == null) TypeName = "Нет названия"; else TypeName = tname;

            if ( (TNo < 0) | (TNo > 255) ) TypeNo = 0;    else TypeNo = TNo;

            if ((ai < 0) | (ai > 255))   AI = 0; else AI = ai;
            if ((ao < 0) | (ao > 255))   AO = 0; else AO = ao;
            if ((di < 0) | (di > 255))   DI = 0; else DI = di;
            if ((d0 < 0) | (d0 > 255))   DO = 0; else DO = d0;

        }
        public typeStructure(LiteDB.ObjectId id, string tname, int TNo, int ai, int ao, int di, int d0) 
        {
            if (tname == null) TypeName = "Нет названия"; else TypeName = tname;

            if ((TNo < 0) | (TNo > 255)) TypeNo = 0; else TypeNo = TNo;

            if ((ai < 0) | (ai > 255)) AI = 0; else AI = ai;
            if ((ao < 0) | (ao > 255)) AO = 0; else AO = ao;
            if ((di < 0) | (di > 255)) DI = 0; else DI = di;
            if ((d0 < 0) | (d0 > 255)) DO = 0; else DO = d0;

            this.Id = id;

        }

    }



    // Данный класс служит для обслуживания  списка типов устройств:
    // имеет на борту текущий список типов 
    // возможность обновляться из FireBase и сохранять в локальное liteDB хранилище
    // возможность выгрузить текущий редактированный список в FireBase 
    // возможность добавлять, редактировать и удалять типы устройств
    class TypeWorking  
    {
        public static List<typeStructure> GlobalTypesList = new List<typeStructure> { };

        public bool LoadListFromLocalDB()  // загружает лист из локального хранилища в коллекцию GlobalList, в случае успеха возвращает True
        {
            try  
            {
                using (var db = new LiteDatabase(@"localTypesDB.db"))
                {
                    var col = db.GetCollection<typeStructure>("types");
                    var result = col.FindAll();

                    GlobalTypesList.Clear();
                    foreach (typeStructure ts in result) GlobalTypesList.Add(ts);
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
                using (var db = new LiteDatabase(@"localTypesDB.db"))
                {
                    var col = db.GetCollection<typeStructure>("types");
                    
                    col.DeleteAll(); 

                    foreach (typeStructure ts in GlobalTypesList) { ts.Id = new LiteDB.ObjectId(); var res = col.Insert(ts); };

                }
                return true;
            }
            catch { };

            return false;
        }

        public bool LoadListFromFireBase(FirebaseDB fDB) // по сути копирует ветку из FireBase в локальную переменную
        {
            FirebaseDB fdb = fDB.NodePath("types/"); // Выбираем ветку

            try
            {
                FirebaseResponse getResponse = fdb.Get(); // читаем из FireBase, и если OK, 
                if (getResponse.Success)
                {
                    string ss = getResponse.JSONContent.ToString();

              //      ss = ss.Substring(ss.IndexOf("[")); ss = ss.Substring(0, ss.Length - 1); // Срезаем заголовок, мешеает

                    List<typeStructure> inList = JsonConvert.DeserializeObject<List<typeStructure>>(ss); // конвертируем в структуру

                    TypeWorking.GlobalTypesList.Clear(); // и копируем в ОЗУ, не в локальную БД
                    foreach (typeStructure ts in inList) TypeWorking.GlobalTypesList.Add(ts);
                }

                return true;     // ну и если все в порядке, вовращаем True
            }
            catch {  };

            return false;
        }

        public bool SaveListToFireBase(FirebaseDB fDB) // сохраняем содержимое коллекции в FireBase (опасная функция - предусмотреть защиту!)
        {
            try
            {
                if (GlobalTypesList.Count > 0) foreach (typeStructure gl in GlobalTypesList) gl.Id = LiteDB.ObjectId.NewObjectId(); // Add timestamp here

                string json = JsonConvert.SerializeObject(GlobalTypesList, Formatting.Indented); // Раскладывает коллекцию на нодлист

                FirebaseDB firebaseDBTeams = fDB.NodePath("types");
                FirebaseResponse putResponse = firebaseDBTeams.Put(json); // Слив в FireBase; // с перезаписью всего нода
            
                return true;
            }
            catch { };

            return false;
        }

        public void refreshTypesTreeView(TreeView dw)
        {
            dw.Nodes.Clear();
            int i = 0;
            foreach (typeStructure t in TypeWorking.GlobalTypesList)
            {
                dw.Nodes.Add(t.TypeName);
                dw.Nodes[i].Nodes.Add("TypeNo: " + t.TypeNo.ToString());
                dw.Nodes[i].Nodes.Add("AI: " + t.AI.ToString());
                dw.Nodes[i].Nodes.Add("AO: " + t.AO.ToString());
                dw.Nodes[i].Nodes.Add("DI: " + t.DI.ToString());
                dw.Nodes[i].Nodes.Add("DO: " + t.DO.ToString());
                i++;

            }
        }
        public void refreshTypesTable(DataGridView dw)
        {
            dw.Rows.Clear();

            foreach (typeStructure t in TypeWorking.GlobalTypesList)
            {

                dw.Rows.Add
                    (
                        t.TypeName,
                        t.TypeNo,
                        t.AI,
                        t.AO,
                        t.DI,
                        t.DO
                    );
            }
        }


    }



    // FirebaseResponse putResponse = firebaseDBTeams.Put(data);
    //FirebaseResponse postResponse = firebaseDBTeams.Post(data);
    //FirebaseResponse patchResponse = firebaseDBTeams
    //    // Use of NodePath to refer path lnager than a single Node  
    //FirebaseResponse deleteResponse = firebaseDBTeams.Delete();
    //   FirebaseResponse getResponse = firebaseDBTeams.Get();
    //   if (getResponse.Success) MessageBox.Show(getResponse.JSONContent); // (getResponse.JSONContent);


}
