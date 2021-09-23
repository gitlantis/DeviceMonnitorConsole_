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
    public partial class EditConsole : MaterialSkin.Controls.MaterialForm
    {
        FirebaseDB fDB = new FirebaseDB("https://mytestproj-1fda7.firebaseio.com/");
        TypeWorking types = new TypeWorking();
        devSettingsWorking dSW = new devSettingsWorking();
        public EditConsole()
        {
            InitializeComponent();
        }


        private bool loadGlobaDevSettingslistFromTable()
        {
            devSettingsWorking.GlobalDevicesList.Clear();

            try
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)  // Must Rework later
                {
                    devSettingsWorking.GlobalDevicesList.Add
                        (new devSettingStructure
                            (
                                dataGridView2.Rows[i].Cells[0].Value.ToString(),
                                dataGridView2.Rows[i].Cells[1].Value.ToString(),
                                int.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString()),
                                int.Parse(dataGridView2.Rows[i].Cells[8].Value.ToString()),
                                bool.Parse(dataGridView2.Rows[i].Cells[9].Value.ToString())
                            )
                        );
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в таблице данных!");
                return false;
            }
        }

         private bool loadGlobaTypeslistFromTable()
        {
            TypeWorking.GlobalTypesList.Clear();

            try
            {
                for (int i = 0; i < dataGridView1.RowCount -1 ; i++)
                {
                    TypeWorking.GlobalTypesList.Add
                    (
                      new typeStructure
                          (
                                dataGridView1.Rows[i].Cells[0].Value.ToString(),
                                int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()),
                                int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                                int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                                int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                                int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())
                        )
                    );

                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в таблице данных!");
                return false;
            }

        }



private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (loadGlobaTypeslistFromTable())
                {
                    if (types.SaveListToLocalDB()) MessageBox.Show("Сохранено в локальную базу данных !");
                        else MessageBox.Show("Ошибка сохранения в локальную базу данных !");
                }
        }

        private void button6_Click(object sender, EventArgs e) 
        {
           
            DialogResult result = MessageBox.Show( 
              "Данная операция изменяет ветку с настройками типов в облачном хранилище, что в свою очередь, затронет все устройства глобально.",
               "Сохранить настройки в FireBase ?",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,
                 MessageBoxOptions.DefaultDesktopOnly
                                                 );

            if (result == DialogResult.Yes)
             if (loadGlobaTypeslistFromTable())
              {
                if (types.SaveListToFireBase(fDB))
                     MessageBox.Show("Сохранено в базу данных FireBase !");
                else MessageBox.Show("Ошибка сохранения в базу данных FireBase !");
              }
        }


        private void materialFlatButton1_Click(object sender, EventArgs e) // Add new button
        {

            if (TypeWorking.GlobalTypesList.Count < 1) { MessageBox.Show("Информация о типах устройств не загружена!"); return; };

            EditDevice addForm = new EditDevice("ВВедите имя устройства", "ВВедите уникальный номер устройства", 11 );
            
            addForm.ShowDialog();
            string devName = addForm.DeviceName;
            string devGUID = addForm.DeviceGUID;
            int typeno = addForm.typeNo;

            if (devGUID.Length > 1)
            {
//                if (dataGridView2.Rows.Count > 0)
//                 dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];

                dataGridView2.Rows.Add(); dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];
                dataGridView2.CurrentRow.Cells[0].Value = devName;
                dataGridView2.CurrentRow.Cells[1].Value = devGUID;

                typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == typeno);
                if (ts != null)
                {
                    dataGridView2.CurrentRow.Cells[2].Value = ts.TypeName;
                    dataGridView2.CurrentRow.Cells[3].Value = ts.TypeNo;
                    dataGridView2.CurrentRow.Cells[4].Value = ts.AI;
                    dataGridView2.CurrentRow.Cells[5].Value = ts.AO;
                    dataGridView2.CurrentRow.Cells[6].Value = ts.DI;
                    dataGridView2.CurrentRow.Cells[7].Value = ts.DO;
                }
                dataGridView2.CurrentRow.Cells[8].Value = 0;
                dataGridView2.CurrentRow.Cells[9].Value = false;
            }


            addForm.Dispose();

        }

        private void EditConsole_Load(object sender, EventArgs e)
        {
            types.LoadListFromLocalDB();
            dSW.LoadListFromLocalDB();
            dSW.refreshDevicesTable(dataGridView2);


          //  DeviceMonnitoringSystem dms = new DeviceMonnitoringSystem();
          //  dms.refreshTypesTreeiew(treeView1);
        }

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            if (types.LoadListFromFireBase(fDB))
            {
                if (TypeWorking.GlobalTypesList.Count > 0)
                {
                    types.refreshTypesTable(dataGridView1);
                }
                else MessageBox.Show("FireBase не cодержит информацию о типах устройств!");
            }
            else MessageBox.Show("Ошибка загрузки из FireBase!");

        }

        private void materialFlatButton2_Click_1(object sender, EventArgs e)
        {
            if (types.LoadListFromLocalDB())
            {
                if (TypeWorking.GlobalTypesList.Count > 0)
                    {
                    types.refreshTypesTable(dataGridView1);
                }
                else MessageBox.Show("Локальная БД не cодержит информацию о типах устройств!"); 
            }
            else MessageBox.Show("Ошибка загрузки из локальной БД!");
        }

        private void materialFlatButton7_Click(object sender, EventArgs e)
        {
            if (types.LoadListFromFireBase(fDB))
            {
                if (TypeWorking.GlobalTypesList.Count > 0)
                {
                    DialogResult result = MessageBox.Show(
                      "Данная операция перезапишет ветку с настройками типов в локальной базе данных значениями из облака FB!",
                        "Сохранить настройки в локальной БД ?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,
                         MessageBoxOptions.DefaultDesktopOnly
                                                         );
                    if (result == DialogResult.Yes)
                        {
                            if (types.SaveListToLocalDB())
                                MessageBox.Show("Синхронизировано в локальную базу данных из FireBase !");
                            else MessageBox.Show("Ошибка сохранения в локальную базу данных !");
                        }
                }
                else MessageBox.Show("FireBase не cодержит информацию о типах устройств!");
            }
            else MessageBox.Show("Ошибка загрузки из FireBase!");

        }

        private void materialFlatButton8_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            button1.Visible = true;
            button10.Visible = true;
            button6.Visible = true;

        }

        private void materialFlatButton4_Click(object sender, EventArgs e) // Save Button
        {
            if (loadGlobaDevSettingslistFromTable())
            {
                if (dSW.SaveListToLocalDB()) MessageBox.Show("Сохранено в локальную базу данных !");
                else MessageBox.Show("Ошибка сохранения в локальную базу данных !");
            }

        }

        private void materialFlatButton3_Click(object sender, EventArgs e) // edit button
        {
            string devName = "ВВедите имя Устройства";
            string devGUID = "ВВедите УИД Устройства";
            int typeno = 0;



            if (TypeWorking.GlobalTypesList.Count < 1) { MessageBox.Show("Информация о типах устройств не загружена!"); return; };


            try
            {
                 devName = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                 devGUID = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                 typeno = int.Parse(dataGridView2.CurrentRow.Cells[3].Value.ToString());
            }
            catch
            {}

            EditDevice addForm = new EditDevice(devName, devGUID, typeno);

            addForm.ShowDialog();
            devName = addForm.DeviceName;
            devGUID = addForm.DeviceGUID;
            typeno = addForm.typeNo;

            if  (devGUID.Length > 1) 
            {
              if (dataGridView2.Rows.Count < 1) dataGridView2.Rows.Add();
                
                dataGridView2.CurrentRow.Cells[0].Value = devName;
                dataGridView2.CurrentRow.Cells[1].Value = devGUID;

                typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == typeno);
                if (ts != null)
                {
                    dataGridView2.CurrentRow.Cells[2].Value = ts.TypeName;
                    dataGridView2.CurrentRow.Cells[3].Value = ts.TypeNo;
                    dataGridView2.CurrentRow.Cells[4].Value = ts.AI;
                    dataGridView2.CurrentRow.Cells[5].Value = ts.AO;
                    dataGridView2.CurrentRow.Cells[6].Value = ts.DI;
                    dataGridView2.CurrentRow.Cells[7].Value = ts.DO;
                }
                if (dataGridView2.CurrentRow.Cells[8].Value == null) dataGridView2.CurrentRow.Cells[8].Value = 0;
                if (dataGridView2.CurrentRow.Cells[9].Value == null) dataGridView2.CurrentRow.Cells[9].Value = false;
            }

            addForm.Dispose();

        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
                 dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
        }

        private void EditConsole_Shown(object sender, EventArgs e)
        {
            materialFlatButton2_Click_1(sender, e);
        }

        private void materialFlatButton10_Click(object sender, EventArgs e) // to firebase
        {
            if (loadGlobaDevSettingslistFromTable())
            {
                DialogResult result = MessageBox.Show(
                    "Данная операция изменяет ветку с списком устройств в облачном хранилище!",
                       "Сохранить список устройств в FireBase ?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,
                        MessageBoxOptions.DefaultDesktopOnly
                                                     );

                if (result == DialogResult.Yes)
                        if (dSW.SaveListToFireBase(fDB, this.Text))
                            MessageBox.Show("Сохранено в базу данных FireBase !");
                        else MessageBox.Show("Ошибка сохранения в базу данных FireBase !");
            }
        }

        private void materialFlatButton9_Click(object sender, EventArgs e)
        {
            if (dSW.LoadListFromFireBase(fDB, this.Text))
            {
                if (devSettingsWorking.GlobalDevicesList.Count > 0)
                {
                    dSW.refreshDevicesTable(dataGridView2);
                }
                else MessageBox.Show("FireBase не cодержит информацию об устройствах!");
            }
            else MessageBox.Show("Ошибка загрузки из FireBase!");

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void materialFlatButton11_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }

        private void materialFlatButton12_Click(object sender, EventArgs e)
        {
            if (loadGlobaTypeslistFromTable())
            {
                if (types.SaveListToLocalDB()) MessageBox.Show("Сохранено в локальную базу данных !");
                else MessageBox.Show("Ошибка сохранения в локальную базу данных !");
            }
        }

        private void materialFlatButton13_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
             "Данная операция изменяет ветку с настройками типов в облачном хранилище, что в свою очередь, затронет все устройства глобально.",
              "Сохранить настройки в FireBase ?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly
                                                );

            if (result == DialogResult.Yes)
                if (loadGlobaTypeslistFromTable())
                {
                    if (types.SaveListToFireBase(fDB))
                        MessageBox.Show("Сохранено в базу данных FireBase !");
                    else MessageBox.Show("Ошибка сохранения в базу данных FireBase !");
                }
        }

        private void materialFlatButton8_Click_1(object sender, EventArgs e)
        {
            enableButtons();

            LoginPass pl = new LoginPass();
            pl.ShowDialog();
            pl.Dispose();
        }

        private void enableButtons()
        {
            dataGridView1.ReadOnly = false;
            materialFlatButton11.Enabled = true;
            materialFlatButton12.Enabled = true;
            materialFlatButton13.Enabled = true;
        }

        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        {

            if (TypeWorking.GlobalTypesList.Count < 1) { MessageBox.Show("Информация о типах устройств не загружена!"); return; };

            EditDevice addForm = new EditDevice("ВВедите имя устройства", "ВВедите уникальный номер устройства", 11);

            addForm.ShowDialog();
            string devName = addForm.DeviceName;
            string devGUID = addForm.DeviceGUID;
            int typeno = addForm.typeNo;

            if (devGUID.Length > 1)
            {
                //                if (dataGridView2.Rows.Count > 0)
                //                 dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];

                dataGridView2.Rows.Add(); dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];
                dataGridView2.CurrentRow.Cells[0].Value = devName;
                dataGridView2.CurrentRow.Cells[1].Value = devGUID;

                typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == typeno);
                if (ts != null)
                {
                    dataGridView2.CurrentRow.Cells[2].Value = ts.TypeName;
                    dataGridView2.CurrentRow.Cells[3].Value = ts.TypeNo;
                    dataGridView2.CurrentRow.Cells[4].Value = ts.AI;
                    dataGridView2.CurrentRow.Cells[5].Value = ts.AO;
                    dataGridView2.CurrentRow.Cells[6].Value = ts.DI;
                    dataGridView2.CurrentRow.Cells[7].Value = ts.DO;
                }
                dataGridView2.CurrentRow.Cells[8].Value = 0;
                dataGridView2.CurrentRow.Cells[9].Value = false;
            }


            addForm.Dispose();
        }

        private void materialFlatButton3_Click_1(object sender, EventArgs e)
        {
            string devName = "ВВедите имя Устройства";
            string devGUID = "ВВедите УИД Устройства";
            int typeno = 0;



            if (TypeWorking.GlobalTypesList.Count < 1) { MessageBox.Show("Информация о типах устройств не загружена!"); return; };


            try
            {
                devName = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                devGUID = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                typeno = int.Parse(dataGridView2.CurrentRow.Cells[3].Value.ToString());
            }
            catch
            { }

            EditDevice addForm = new EditDevice(devName, devGUID, typeno);

            addForm.ShowDialog();
            devName = addForm.DeviceName;
            devGUID = addForm.DeviceGUID;
            typeno = addForm.typeNo;

            if (devGUID.Length > 1)
            {
                if (dataGridView2.Rows.Count < 1) dataGridView2.Rows.Add();

                dataGridView2.CurrentRow.Cells[0].Value = devName;
                dataGridView2.CurrentRow.Cells[1].Value = devGUID;

                typeStructure ts = TypeWorking.GlobalTypesList.Find(x => x.TypeNo == typeno);
                if (ts != null)
                {
                    dataGridView2.CurrentRow.Cells[2].Value = ts.TypeName;
                    dataGridView2.CurrentRow.Cells[3].Value = ts.TypeNo;
                    dataGridView2.CurrentRow.Cells[4].Value = ts.AI;
                    dataGridView2.CurrentRow.Cells[5].Value = ts.AO;
                    dataGridView2.CurrentRow.Cells[6].Value = ts.DI;
                    dataGridView2.CurrentRow.Cells[7].Value = ts.DO;
                }
                if (dataGridView2.CurrentRow.Cells[8].Value == null) dataGridView2.CurrentRow.Cells[8].Value = 0;
                if (dataGridView2.CurrentRow.Cells[9].Value == null) dataGridView2.CurrentRow.Cells[9].Value = false;
            }

            addForm.Dispose();
        }

        private void materialFlatButton5_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
                dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
        }

        private void materialFlatButton4_Click_1(object sender, EventArgs e)
        {
            if (loadGlobaDevSettingslistFromTable())
            {
                if (dSW.SaveListToLocalDB()) MessageBox.Show("Сохранено в локальную базу данных !");
                else MessageBox.Show("Ошибка сохранения в локальную базу данных !");
            }
        }

        private void materialFlatButton9_Click_1(object sender, EventArgs e)
        {
            if (dSW.LoadListFromFireBase(fDB, this.Text))
            {
                if (devSettingsWorking.GlobalDevicesList.Count > 0)
                {
                    dSW.refreshDevicesTable(dataGridView2);
                }
                else MessageBox.Show("FireBase не cодержит информацию об устройствах!");
            }
            else MessageBox.Show("Ошибка загрузки из FireBase!");
        }

        private void materialFlatButton10_Click_1(object sender, EventArgs e)
        {
            if (loadGlobaDevSettingslistFromTable())
            {
                DialogResult result = MessageBox.Show(
                    "Данная операция изменяет ветку с списком устройств в облачном хранилище!",
                       "Сохранить список устройств в FireBase ?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,
                        MessageBoxOptions.DefaultDesktopOnly
                                                     );

                if (result == DialogResult.Yes)
                    if (dSW.SaveListToFireBase(fDB, this.Text))
                        MessageBox.Show("Сохранено в базу данных FireBase !");
                    else MessageBox.Show("Ошибка сохранения в базу данных FireBase !");
            }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            if (types.LoadListFromLocalDB())
            {
                if (TypeWorking.GlobalTypesList.Count > 0)
                {
                    types.refreshTypesTable(dataGridView1);
                }
                else MessageBox.Show("Локальная БД не cодержит информацию о типах устройств!");
            }
            else MessageBox.Show("Ошибка загрузки из локальной БД!");
        }

        private void materialFlatButton6_Click_1(object sender, EventArgs e)
        {
            if (types.LoadListFromFireBase(fDB))
            {
                if (TypeWorking.GlobalTypesList.Count > 0)
                {
                    types.refreshTypesTable(dataGridView1);
                }
                else MessageBox.Show("FireBase не cодержит информацию о типах устройств!");
            }
            else MessageBox.Show("Ошибка загрузки из FireBase!");
        }

        private void materialFlatButton7_Click_1(object sender, EventArgs e)
        {
            if (types.LoadListFromFireBase(fDB))
            {
                if (TypeWorking.GlobalTypesList.Count > 0)
                {
                    DialogResult result = MessageBox.Show(
                      "Данная операция перезапишет ветку с настройками типов в локальной базе данных значениями из облака FB!",
                        "Сохранить настройки в локальной БД ?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2,
                         MessageBoxOptions.DefaultDesktopOnly
                                                         );
                    if (result == DialogResult.Yes)
                    {
                        if (types.SaveListToLocalDB())
                            MessageBox.Show("Синхронизировано в локальную базу данных из FireBase !");
                        else MessageBox.Show("Ошибка сохранения в локальную базу данных !");
                    }
                }
                else MessageBox.Show("FireBase не cодержит информацию о типах устройств!");
            }
            else MessageBox.Show("Ошибка загрузки из FireBase!");
        }

        private void materialFlatButton14_Click(object sender, EventArgs e)
        {
            PermessionForm pf = new PermessionForm(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7")) ;
            pf.ShowDialog();
            pf.Dispose();
        }
    }
}
