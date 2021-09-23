using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteDB;
using System.Threading;

namespace softHardwareAdmin
{


    public partial class DeviceMonnitoringSystem : MaterialSkin.Controls.MaterialForm
    {
        FirebaseDB fDB = new FirebaseDB("https://mytestproj-1fda7.firebaseio.com/");
        TypeWorking types = new TypeWorking();
        devSettingsWorking dSW = new devSettingsWorking();

        List<dataStructure> _list = new List<dataStructure>();

        string globalEndPointID = "ID_ART_SAMARQAND_OBJECT_18_00001";


        public DeviceMonnitoringSystem()
        {
            InitializeComponent();
        }

        void refreshTypesAndDevices()
        {
            int i = 0;
            types.LoadListFromLocalDB();
            dSW.LoadListFromLocalDB();

            treeView1.Nodes.Clear();

            foreach (devSettingStructure ds in devSettingsWorking.GlobalDevicesList)
            {
                treeView1.Nodes.Add(ds.DeviceName + " : " + ds.DeviceGUID);
                i = treeView1.Nodes.Count - 1;


                typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == ds.TypeNo);
                    if (ts != null)
                    {
                        if (ds.Enabled) treeView1.Nodes[i].ForeColor = Color.Green; else treeView1.Nodes[i].ForeColor = Color.Red;
                    treeView1.Nodes[i].Nodes.Add("тип устройства "           + ts.TypeName +" : " + ds.TypeNo.ToString());
                        treeView1.Nodes[i].Nodes.Add("аналоговых входов "        + ts.AI.ToString());
                        treeView1.Nodes[i].Nodes.Add("аналоговых выходов "       + ts.AO.ToString());
                        treeView1.Nodes[i].Nodes.Add("цифровых входов "          + ts.DI.ToString());
                        treeView1.Nodes[i].Nodes.Add("цифровых выходов "         + ts.DO.ToString());
                    }
                    else treeView1.Nodes[i].Nodes.Add("тип устройства не обнаружен " + ds.TypeNo.ToString());

                treeView1.Nodes[i].Nodes.Add("Период считывания данных " + ds.RefreshTime.ToString());
                if (ds.Enabled) treeView1.Nodes[i].Nodes.Add("Задействовано"); else treeView1.Nodes[i].Nodes.Add("Отключено");
            }


        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Form loginForm = new Login();
            loginForm.ShowDialog();
            loginForm.Dispose();
        }

        private void DeviceMonnitorSystem_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(StartScreen));
            t.Start();
            Thread.Sleep(1000);
            t.Abort();

            this.BringToFront();
            this.Activate();

            types.LoadListFromLocalDB();
            dSW.LoadListFromLocalDB();

            //            refreshTypesTreeiew(treeView1);
        }

        private void StartScreen()
        {
            Application.Run(new SplashScreen());
        }




        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            DataWorking dw = new DataWorking();
            string guid;

            TreeNode node = treeView1.SelectedNode;
            while (node.Parent != null)
            {
                node = node.Parent;
            }

            guid = devSettingsWorking.GlobalDevicesList[node.Index].DeviceGUID;

            //-----------------------------------------------------------------
            textBox2.Text = devSettingsWorking.GlobalDevicesList[node.Index].TypeNo.ToString();
            textBox3.Text = devSettingsWorking.GlobalDevicesList[node.Index].DeviceGUID;
            //-----------------------------------------------------------------

            _list.Clear();
            _list = dw.LoadListFromLocalDB(guid, dateTimePicker1.Value.Date);

            _list.Sort(delegate (dataStructure ds1, dataStructure ds2) { return ds1.Id.CreationTime.CompareTo(ds2.Id.CreationTime); });

            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();

            foreach (dataStructure ds in _list)
            {
                dataGridView2.Rows.Add(
                    ds.DeviceGUID, 
                    ds.Id.CreationTime.ToLongTimeString()
                                      );
                
            }
  
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dataStructure dst = new dataStructure();
            DataWorking dw = new DataWorking();

            int cnt = dataGridView3.RowCount;
            
            for (int i=0;i<cnt;i++)
            {
                if (dataGridView3.Rows[i].Cells[1].Value != null) dst.AI.Add(UInt16.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString()));
                if (dataGridView3.Rows[i].Cells[2].Value != null) dst.AO.Add(UInt16.Parse(dataGridView3.Rows[i].Cells[2].Value.ToString()));
                if (dataGridView3.Rows[i].Cells[3].Value != null) dst.DI.Add(UInt16.Parse(dataGridView3.Rows[i].Cells[3].Value.ToString()));
                if (dataGridView3.Rows[i].Cells[4].Value != null) dst.DO.Add(UInt16.Parse(dataGridView3.Rows[i].Cells[4].Value.ToString()));

            }
            dst.DeviceGUID = textBox3.Text;
            dst.status = UInt32.Parse(dataGridView3.Rows[0].Cells[5].Value.ToString() );

            dst.Id = new LiteDB.ObjectId();
            dw.SaveElementToLocalDB(dst);

            treeView1_AfterSelect(sender, new TreeViewEventArgs(treeView1.SelectedNode));


        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int ind = dataGridView2.CurrentCell.RowIndex;
            
            UInt16? _ai,_ao,_di,_do;
            UInt32? _status;

            int maxcount = _list[ind].AI.Count;
            if (maxcount < _list[ind].AO.Count) maxcount = _list[ind].AO.Count;
            if (maxcount < _list[ind].DI.Count) maxcount = _list[ind].DI.Count;
            if (maxcount < _list[ind].DO.Count) maxcount = _list[ind].DO.Count;

            dataGridView1.Rows.Clear();
            _status = _list[ind].status;
            for (int i=0;i<maxcount;i++)
            {
                if (i < _list[ind].AI.Count) _ai = _list[ind].AI[i]; else _ai = null;
                if (i < _list[ind].AO.Count) _ao = _list[ind].AO[i]; else _ao = null;
                if (i < _list[ind].DI.Count) _di = _list[ind].DI[i]; else _di = null;
                if (i < _list[ind].DO.Count) _do = _list[ind].DO[i]; else _do = null;

                dataGridView1.Rows.Add(i, _ai, _ao, _di, _do, _status);

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            int ds = int.Parse(textBox2.Text);

            UInt16? _ai, _ao, _di, _do;
            UInt32? _status;

            typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == ds);

            dataGridView3.Rows.Clear();

            if (ts != null)
            {
                int maxcount = ts.AI;
                if (maxcount < ts.AO) maxcount = ts.AO;
                if (maxcount < ts.DI) maxcount = ts.DI;
                if (maxcount < ts.DO) maxcount = ts.DO;

                for (int i = 0; i < maxcount; i++)
                {
                    if (maxcount <= ts.AI) _ai = 0; else _ai = null;
                    if (maxcount <= ts.AO) _ao = 0; else _ao = null;
                    if (maxcount <= ts.DI) _di = 0; else _di = null;
                    if (maxcount <= ts.DO) _do = 0; else _do = null;
                    dataGridView3.Rows.Add(i,_ai,_ao,_di,_do, 0);
                }




            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataWorking dw = new DataWorking();
            dw.UploadLocalDBtoFireBase(globalEndPointID);
        }
       
        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            refreshTypesAndDevices();
        }

        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        {
            Form loginForm = new Login();
            loginForm.ShowDialog();
            loginForm.Dispose();
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            DataWorking dw = new DataWorking();
            string guid;

            TreeNode node = treeView1.SelectedNode;
            while (node.Parent != null)
            {
                node = node.Parent;
            }

            guid = devSettingsWorking.GlobalDevicesList[node.Index].DeviceGUID;

            //-----------------------------------------------------------------
            textBox2.Text = devSettingsWorking.GlobalDevicesList[node.Index].TypeNo.ToString();
            textBox3.Text = devSettingsWorking.GlobalDevicesList[node.Index].DeviceGUID;
            //-----------------------------------------------------------------

            _list.Clear();
            _list = dw.LoadListFromLocalDB(guid, dateTimePicker1.Value.Date);

            _list.Sort(delegate (dataStructure ds1, dataStructure ds2) { return ds1.Id.CreationTime.CompareTo(ds2.Id.CreationTime); });

            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();

            foreach (dataStructure ds in _list)
            {
                dataGridView2.Rows.Add(
                    ds.DeviceGUID,
                    ds.Id.CreationTime.ToLongTimeString()
                                      );

            }
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            UserConsole userForm = new UserConsole();
            userForm.ShowDialog();
            userForm.Dispose();

        }
    }
}

