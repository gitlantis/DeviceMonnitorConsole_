using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetwayAndAPI
{
    public partial class MainForm : MaterialSkin.Controls.MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            UserConsole uc = new UserConsole();
            uc.ShowDialog();
            uc.Dispose();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            ControlDevices uc = new ControlDevices();
            uc.ShowDialog();
            uc.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestFB uc = new TestFB();
            uc.ShowDialog();
            uc.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var chipFile = Path.Combine(Directory.GetCurrentDirectory(), Constants.ChipFile);
            if (File.Exists(chipFile)) File.Delete(chipFile);
        }
    }
}
