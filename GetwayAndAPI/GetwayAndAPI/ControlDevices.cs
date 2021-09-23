using GetwayAndAPI.Models;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetwayAndAPI
{    
    public partial class ControlDevices : MaterialSkin.Controls.MaterialForm
    {
        private string _oltString;
        public ControlDevices()
        {
            InitializeComponent();
        }

        private void ControlDevices_Load(object sender, EventArgs e)
        {
            this.getDevices();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Task delete = this.deleteDevice(Guid.Parse(materialSingleLineTextField2.Text));
        }

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            Device drd = new Device();

            drd.DeviceGuid = Guid.Parse(materialSingleLineTextField2.Text);
            drd.Name = materialSingleLineTextField3.Text;
            drd.Description = materialSingleLineTextField4.Text;
            drd.IsActive = materialCheckBox1.Checked;

            this.addDevice(drd);            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "Delete":
                    if (MessageBox.Show($"Дейстивително вы хотите удалит ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Task deleteingTask = this.deleteDevice(Guid.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        this.getDevices();
                    }
                    break;
                case "Save":

                    var model = new Device();

                    model.DeviceGuid = Guid.Parse(dataGridView1.Rows[e.RowIndex].Cells["DeviceGuid"].Value.ToString());
                    model.Name = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    model.Description = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                    model.IsActive = (bool)dataGridView1.Rows[e.RowIndex].Cells["IsActive"].Value;

                    Task editingTask = this.editDevice(model);
                    break;
                case "RemoveData":
                    Task clearData = this.clearData(Guid.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    break;
            }
        }

        private async Task deleteDevice(Guid device)
        {
            TranCiever tr = new TranCiever();
            var connects = await tr.PostAsync<Guid>("Device/DeleteDevice", device);
        }

        private async Task addDevice(Device device)
        {
            TranCiever tr = new TranCiever();
            var result = await tr.PostAsync<Device>("Device/AddDevice", device);
            //materialLabel1.Text = result.DeviceGuid.ToString();
            this.getDevices();
        }

        private async Task editDevice(Device device)
        {
            TranCiever tr = new TranCiever();
            var result = await tr.PostAsync<Device>("Device/EditDevice", device);
            //materialLabel1.Text = result.DeviceGuid.ToString();
            this.getDevices();
        }

        private async Task getDevices()
        {
            TranCiever tr = new TranCiever();
            var result = await tr.PostAsync<List<Device>>("Device/GetDevices", null);
            this.loadData(result);            
        }

        private async Task clearData(Guid guid)
        {
            TranCiever tr = new TranCiever();
            var result = await tr.PostGuiderAsync<Guid>("DeviceData/DeleteDataByDevice", guid);
            if (result != null)
                MessageBox.Show($"{guid.ToString()} data cleared");
        }

        private void loadData(List<Device> model)
        {

            int i = 0;
            dataGridView1.Rows.Clear();
            var rows = model.Count;

            dataGridView1.Rows.Add(rows);

            foreach (var m in model)
            {
                try
                {
                    dataGridView1.Rows[i].Cells[0].Value = m.DeviceGuid.ToString();
                    dataGridView1.Rows[i].Cells[1].Value = m.Name;
                    dataGridView1.Rows[i].Cells[2].Value = m.Description;
                    dataGridView1.Rows[i].Cells[3].Value = m.IsActive;                    
                    dataGridView1.Rows[i].Cells[4].Value = "Удалит";
                    dataGridView1.Rows[i].Cells[5].Value = "Сахранит";
                    dataGridView1.Rows[i].Cells[6].Value = "Стерит данные";
                }
                catch { }

                i++;
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PermessionForm pf = new PermessionForm(Guid.Parse(dataGridView1.Rows[e.RowIndex].Cells["DeviceGuid"].Value.ToString()), dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString());
                pf.ShowDialog();
                pf.Dispose();
            }
            catch { }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            _oltString = materialSingleLineTextField2.Text = Guid.NewGuid().ToString();
        }

        private void materialSingleLineTextField2_KeyUp(object sender, KeyEventArgs e)
        {
            materialSingleLineTextField2.Text = _oltString;
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            this.getDevices();
        }
    }
}