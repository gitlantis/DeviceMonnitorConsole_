using softHardwareAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softHardwareAdmin
{
    public partial class PermessionForm : MaterialSkin.Controls.MaterialForm
    {
        private readonly Guid _deviceGuid;
        private List<OrgUser> _users;
        public PermessionForm()
        {
            InitializeComponent();
        }
        public PermessionForm(Guid deviceGuid)
        {
            _deviceGuid = deviceGuid;
            InitializeComponent();
        }

        private void PermessionForm_Load(object sender, EventArgs e)
        {
            Task getUsers = this.getUsers();

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            var du = new DeviceUsers();
            var g = new List<Guid>();

            int j = 0;
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[4].Value == true)
                {
                    g.Add(Guid.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()));
                }
            }

            du.UserGuid = g.ToArray();
            du.DeviceGuid = _deviceGuid;
            Task post = this.postData(du);

        }

        private async Task postData(DeviceUsers du)
        {
            TranCiever tr = new TranCiever();

            var connects = await tr.PostAsync<DeviceUsers>("Device/ConnectUsers", du);

            this.loadData(_users, connects);
        }

        private async Task getUsers()
        {
            TranCiever tr = new TranCiever();

            var result = await tr.PostAsync<List<OrgUser>>("User/GetUsers", null);
            var connects = await tr.PostAsync<DeviceUsers>("Device/GetDeviceUsers", _deviceGuid);
            this.loadData(result, connects);
        }

        private async Task addData(Guid device)
        {
            var portion = new Dictionary<string, decimal>();
            portion["val01"] = 1.2M;
            portion["val02"] = 2.2M;
            portion["val03"] = 3.2M;

            var data = new DeviceRawData();
            data.DeviceGUID = device;
            data.DataCreatedTime = DateTime.Now;
            data.DeviceOnceDataPortion = portion;

            TranCiever tr = new TranCiever();
            var connects = await tr.PostAsync<DeviceRawData>("DeviceData/AddData", data);            
        }

        private async Task deleteDevice(Guid device)
        {
            TranCiever tr = new TranCiever();            
            var connects = await tr.PostAsync<Guid>("Device/DeleteDevice", device);            
        }

        private async Task deleteDeviceData(Guid device)
        {
            TranCiever tr = new TranCiever();
            var connects = await tr.PostAsync<Guid>("DeviceData/DeleteDataByDevice", device);
        }

        private async Task deleteData(Guid device)
        {
            TranCiever tr = new TranCiever();
            var connects = await tr.PostAsync<Guid>("DeviceData/DeleteData", device);
        }

        private void loadData(List<OrgUser> model, DeviceUsers connects)
        {

            int i = 0;
            dataGridView1.Rows.Clear();
            var rows = model.Count;
            dataGridView1.Rows.Add(rows);

            foreach (var m in model)
            {
                dataGridView1.Rows[i].Cells[0].Value = m.UserGuid.ToString();
                dataGridView1.Rows[i].Cells[1].Value = m.FirstName;
                dataGridView1.Rows[i].Cells[2].Value = m.LastName;
                dataGridView1.Rows[i].Cells[3].Value = m.Username;

                var exist = false;
                try
                {
                    exist = connects.UserGuid.Contains(m.UserGuid);
                }
                catch { }
                
                dataGridView1.Rows[i].Cells[4].Value = exist;

                i++;
            }

        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Task delete = this.deleteDevice(Guid.Parse(materialSingleLineTextField1.Text));
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            Task add = this.addData(Guid.Parse(materialSingleLineTextField1.Text));
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            Task delete = this.deleteDeviceData(Guid.Parse(materialSingleLineTextField1.Text));
        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            Task delete = this.deleteData(Guid.Parse(materialSingleLineTextField1.Text));
        }
    }
}
