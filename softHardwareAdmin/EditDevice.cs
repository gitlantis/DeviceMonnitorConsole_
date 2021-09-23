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
    public partial class EditDevice : MaterialSkin.Controls.MaterialForm
    {
        public string DeviceName;
        public string DeviceGUID;
        public int typeNo;

        public EditDevice(string _DeviceName, string _DeviceGUID, int _typeNo)
        {
            InitializeComponent();

            textBox1.Text = _DeviceName; DeviceName = _DeviceName;
            textBox2.Text = _DeviceGUID; DeviceGUID = _DeviceGUID;
            typeNo = _typeNo;

            comboBox1.Items.Clear();
            if (TypeWorking.GlobalTypesList.Count > 0)
            {
                foreach (typeStructure ts in TypeWorking.GlobalTypesList)
                {
                    comboBox1.Items.Add(ts.TypeName + " : " + ts.TypeNo.ToString());
                }
                
                int t = TypeWorking.GlobalTypesList.FindIndex (x => x.TypeNo == typeNo);

                if (t >= 0) comboBox1.SelectedIndex = t;


            }

        }


    private void EditDevice_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            DeviceName = textBox1.Text;
            DeviceGUID = textBox2.Text;
            try { typeNo = TypeWorking.GlobalTypesList[comboBox1.SelectedIndex].TypeNo; }
            catch { typeNo = 0; };

            this.Close();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            DeviceGUID = "";
            this.Close();
        }
    }
}
