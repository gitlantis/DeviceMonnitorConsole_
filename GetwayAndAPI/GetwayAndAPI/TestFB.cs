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
using GetwayAndAPI.Models;

namespace GetwayAndAPI
{
    public partial class TestFB : Form
    {
        private IFirebaseClient client;
        private readonly IFirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = "CZPfXMxEIxkOhl41yzZREI890Uu2HMnYrOM0JCTB",//"OA4QKAKnS5wwys2hLMz47QIwutkh1x0UYT1Gc8RY",
            BasePath = "https://testbase-bcc86-default-rtdb.firebaseio.com/"//"https://devconsole-f3fdb-default-rtdb.firebaseio.com/"
        };
        public TestFB()
        {
            InitializeComponent();
        }

        private void TestFB_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new FirebaseClient(fbc);
            var res = client.GetAsync("InputData/08d91048-e36e-4a73-85a4-f1065bbcdd71").Result.Body;
            var config = JsonSerializer.Deserialize<Dictionary<string, Dictionary<DateTime, decimal[]>>>(res);

            foreach (var vaa in config)
            {
                foreach (var d in vaa.Value)
                {
                    foreach (var a in d.Value)
                    {
                        richTextBox1.Text += d.Key + "->" + a + "\n";
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            client = new FirebaseClient(fbc);
            var de = new int[27];
            var rnd = new Random();
            for (int i = 0; i < 27; i++) de[i] = rnd.Next(0, 999);

            var dt = DateTime.Now.AddHours(3).ToString("yyyy-MM-ddTHH:mm:ss");            
            var v = new Dictionary<string, int[]>
            {
                [dt] = de
            };

            var res = client.Push($"InputData/{textBox1.Text}", v);
            richTextBox2.AppendText($"\r\ndata -> [{string.Join(", ", de)}]");
            richTextBox2.AppendText("\r\n"+res.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            {
                this.addData();
            }
            
        }

        private async Task addData()
        {
            TranCiever tr = new TranCiever();

            var de = new decimal[27];
            var rnd = new Random();
            var data = new DeviceRawData();
            for (int i = 0; i < 27; i++) de[i] = rnd.Next(0, 999);

            data.DeviceGUID = Guid.Parse(textBox1.Text);
            data.DataCreatedTime = DateTime.Now.AddHours(3);
            data.Data = de;
            var result = await tr.PostGuiderAsync<DeviceRawData>("DeviceData/AddData", data);
            richTextBox2.AppendText($"\r\ndata -> [{string.Join(", ", de)}]");
            richTextBox2.AppendText($"\r\nresponse -> {result.ToString()}");
        }
    }
}
