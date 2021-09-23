using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using FireSharp.Response;


namespace GateWaySIM800
{



    public partial class Form1 : Form
    {
        List<DeviceRawData> glRawData = new List<DeviceRawData>();

        private IFirebaseClient client;
        private readonly IFirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = "CZPfXMxEIxkOhl41yzZREI890Uu2HMnYrOM0JCTB",        //"OA4QKAKnS5wwys2hLMz47QIwutkh1x0UYT1Gc8RY",
            BasePath = "https://testbase-bcc86-default-rtdb.firebaseio.com/"//"https://devconsole-f3fdb-default-rtdb.firebaseio.com/"
        };

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
             client = new FirebaseClient(fbc);
            try
            {
                var res = client.GetAsync("InputDataNew/").Result.Body;

                var config = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, tempData>>>(res);


                List<DeviceRawData> tList = new List<DeviceRawData>(); tList.Clear();

                DeviceRawData tDrd = new DeviceRawData();

                if (config == null) return;

                int i = 0, j = 0;


                treeView1.Nodes.Clear();

                foreach (var lvl1 in config)
                {
                    Guid gd = Guid.Empty;
                    try { gd = Guid.Parse(lvl1.Key); } catch 
                    {
                    };

                    treeView1.Nodes.Add(gd.ToString()); j = 0; 

                    foreach (var lvl2 in lvl1.Value)
                    {
                       glRawData.Add(new DeviceRawData(lvl1.Key, gd, lvl2.Value.Date, lvl2.Value, lvl2.Key));
                        tDrd = new DeviceRawData(lvl1.Key, gd, lvl2.Value.Date, lvl2.Value, lvl2.Key);
                         treeView1.Nodes[i].Nodes.Add(lvl2.Key.ToString());


                        treeView1.Nodes[i].Nodes[j].Nodes.Add("METADATA");
                        foreach (string dd in lvl2.Value.METADATA) treeView1.Nodes[i].Nodes[j].Nodes[0].Nodes.Add(dd);

                        treeView1.Nodes[i].Nodes[j].Nodes.Add("AI");
                        foreach (decimal dd in lvl2.Value.AI) treeView1.Nodes[i].Nodes[j].Nodes[1].Nodes.Add(dd.ToString());

                        treeView1.Nodes[i].Nodes[j].Nodes.Add("AO");
                        foreach (decimal dd in lvl2.Value.AO) treeView1.Nodes[i].Nodes[j].Nodes[2].Nodes.Add(dd.ToString());

                        treeView1.Nodes[i].Nodes[j].Nodes.Add("DI");
                        foreach (decimal dd in lvl2.Value.DI) treeView1.Nodes[i].Nodes[j].Nodes[3].Nodes.Add(dd.ToString());

                        treeView1.Nodes[i].Nodes[j].Nodes.Add("DO");
                        foreach (decimal dd in lvl2.Value.DO) treeView1.Nodes[i].Nodes[j].Nodes[4].Nodes.Add(dd.ToString());

                        j++;

                    }
                    tList.Add(new DeviceRawData(tDrd.guidName, tDrd.DeviceGUID, tDrd.DataCreatedTime, tDrd.tData, tDrd.fbHash));
                    treeView1.Nodes[i].Expand();
                    i++;
                }

                foreach (DeviceRawData dRD in tList) if (dRD.DeviceGUID != Guid.Empty) addData(dRD);
            }
            catch
            { };
        }


        private async Task addData(DeviceRawData data)
        {
            TranCiever tr = new TranCiever();
            var result = await tr.PostGuiderAsync<DeviceRawData>("DeviceData/AddData", data);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        public bool SaveElementToLocalDB(DeviceRawData ds)
        {
            try
            {
                using (var db = new LiteDB.LiteDatabase(@"localDevDataDB.db"))
                {
                    var col = db.GetCollection<DeviceRawData>("data");
                    var res = col.Insert(ds);
                }
                return true;
            }
            catch { };

            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client = new FirebaseClient(fbc);
            try
            {

                foreach (DeviceRawData drd in glRawData)
                    if (SaveElementToLocalDB(drd))
                    {
                        client.DeleteAsync("InputDataNew/" + drd.guidName + "/" + drd.fbHash);
                    };

                glRawData.Clear();
            }
            catch { };

        }

        public List<DeviceRawData> LoadListFromLocalDB( DateTime DT)
        {
            List<DeviceRawData> lst = new List<DeviceRawData>();
            lst.Clear();
            try
            {
                using (var dbb = new LiteDB.LiteDatabase(@"localDevDataDB.db"))
                {
                    var col = dbb.GetCollection<DeviceRawData>("data");
                    col.EnsureIndex(x => x.DeviceGUID);
                    var ans = col.Find(x => x.DataCreatedTime.Date.Equals(DT.Date));
                    foreach (var ts in ans) lst.Add(ts);
                }
            }
            catch { };

            return lst;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;

            List<DeviceRawData> lst = LoadListFromLocalDB(dt);
            List<string> guids = new List<string>();
            guids.Clear();

            try
            {

                foreach (DeviceRawData drd in lst)
                    if (!guids.Contains(drd.DeviceGUID.ToString())) guids.Add(drd.DeviceGUID.ToString());

                if (guids.Count == 0) return;

                treeView2.Nodes.Clear();

                foreach (string ss in guids) treeView2.Nodes.Add(ss);

                foreach (DeviceRawData drd in lst)
                    for (int i = 0; i < treeView2.Nodes.Count; i++)
                        if (treeView2.Nodes[i].Text == drd.DeviceGUID.ToString())
                        {
                            treeView2.Nodes[i].Nodes.Add(drd.DataCreatedTime.ToString());
                            //foreach (decimal dd in drd.Data)
                            //    treeView2.Nodes[i].Nodes[treeView2.Nodes[i].Nodes.Count - 1].Nodes.Add(dd.ToString());
                        };
            }
            catch { };

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = int.Parse(textBox1.Text) * 1000;
                button1_Click(button1, e);
            }
            catch { };
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try { 
            timer2.Interval = int.Parse(textBox2.Text) * 1000;
            button3_Click(button3, e);
                }
            catch { };
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            timer2.Enabled = checkBox2.Checked;
            
        }



        private void button2_Click_1(object sender, EventArgs e)
        {

            List<tempData> tdt = new List<tempData>();
            tdt.Clear();
            client = new FirebaseClient(fbc);

            var res = client.GetAsync("InputDataNew/").Result.Body;

            var config = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, tempData>>>(res);

   


            //                var config = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Dictionary<DateTime, decimal[]>>>>(res);

            //                List<DeviceRawData> tList = new List<DeviceRawData>(); tList.Clear();

            //                DeviceRawData tDrd = new DeviceRawData();

            //if (config == null) return;

            //client = new FirebaseClient(fbc);
            //client.DeleteAsync("InputDataNew");
            //tempData td = new tempData();





        }
    }
}
