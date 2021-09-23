using softHardwareAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softHardwareAdmin
{
    public partial class UserConsole : MaterialSkin.Controls.MaterialForm
    {
        public UserConsole()
        {
            InitializeComponent();
        }

        private void UserConsole_Load(object sender, EventArgs e)
        {
            Task ignoredAwaitableResult = this.getUsers();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            var model = new OrgUser();

            model.FirstName = materialSingleLineTextField1.Text;
            model.LastName = materialSingleLineTextField2.Text;
            model.Username = materialSingleLineTextField3.Text;
            model.Password = materialSingleLineTextField4.Text;
            model.Role = comboBox1.Text;
            model.IsActive = checkBox1.Checked;

            Task ignoredAwaitableResult = this.addUser(model);
        }

        private async Task getUsers()
        {
            TranCiever tr = new TranCiever();

            var result = await tr.PostAsync<List<OrgUser>>("User/GetUsers", null);
            this.loadData(result);
        }
        private void loadData(List<OrgUser> model)
        {

            int i = 0;
            dataGridView1.Rows.Clear();
            var rows = model.Count;
            //if (model.Count>1) rows = model.Count-1;
            dataGridView1.Rows.Add(rows);

            foreach (var m in model)
            {
                dataGridView1.Rows[i].Cells[0].Value = m.UserGuid.ToString();
                dataGridView1.Rows[i].Cells[1].Value = m.FirstName;
                dataGridView1.Rows[i].Cells[2].Value = m.LastName;
                dataGridView1.Rows[i].Cells[3].Value = m.Username;
                dataGridView1.Rows[i].Cells[4].Value = m.Password;
                dataGridView1.Rows[i].Cells[5].Value = m.Role;
                dataGridView1.Rows[i].Cells[6].Value = m.IsActive;
                dataGridView1.Rows[i].Cells[7].Value = "Удалит";
                dataGridView1.Rows[i].Cells[8].Value = "Сахранит";

                i++;
            }

        }

        private async Task editUser(OrgUser model)
        {
            TranCiever tr = new TranCiever();

            var result = await tr.PostAsync<OrgUser>("User/EditUser", model);
            this.getUsers();
        }

        private async Task deleteUser(string guid)
        {
            TranCiever tr = new TranCiever();

            var result = await tr.PostAsync<string>("User/DeleteUser", guid);
            this.getUsers();
        }
        private async Task addUser(OrgUser model)
        {
            TranCiever tr = new TranCiever();

            var result = await tr.PostGuiderAsync<OrgUser>("User/AddUser", model);

            if (result != null)
            {
                model.FirstName = "";
                model.LastName = "";
                model.Username = "";
                model.Password = "";
                model.Role = "User";
                model.IsActive = false;

                materialSingleLineTextField1.Text = "";
                materialSingleLineTextField2.Text = "";
                materialSingleLineTextField3.Text = "";
                materialSingleLineTextField4.Text = "";
                comboBox1.Text = "User";
                checkBox1.Checked = true;

            }
            this.getUsers();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "Delete":
                    if (MessageBox.Show($"Дейстивително вы хотите удалит ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Task deleteingTask = this.deleteUser(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    }
                    break;
                case "Save":

                    var model = new OrgUser();                    

                    model.UserGuid = Guid.Parse(dataGridView1.Rows[e.RowIndex].Cells["UserGuid"].Value.ToString());
                    model.FirstName = dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                    model.LastName = dataGridView1.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
                    model.Username = dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
                    model.Password = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                    model.Role = dataGridView1.Rows[e.RowIndex].Cells["Role"].Value.ToString();
                    
                    model.IsActive = (bool)dataGridView1.Rows[e.RowIndex].Cells["IsActive"].Value;
                    

                    Task editingTask = this.editUser(model);

                    break;
            }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Task ignoredAwaitableResult = this.getUsers();
        }
    }


}
