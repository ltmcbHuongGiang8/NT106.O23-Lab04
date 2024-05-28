using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab4_ltmcb
{
    public partial class Bai5_Lab4 : Form
    {
        public Bai5_Lab4()
        {
            InitializeComponent();
        }
        private readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(@"https://nt106.uitiot.vn")
        };
        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            HttpResponseMessage response = await httpClient.PostAsync("auth/token", formData);
            using (response)
            {
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(res);
                    if (jsonResponse["access_token"] != null)
                    {
                        string tokenType = jsonResponse["token_type"].ToString();
                        string accessToken = jsonResponse["access_token"].ToString();

                        richTextBox1.AppendText(tokenType + " " + accessToken + "\n");
                        richTextBox1.AppendText("Đăng nhập thành công\n");
                    }
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    JObject errorJson = JObject.Parse(errorResponse);
                    string detail = errorJson["detail"].ToString();
                    richTextBox1.AppendText(detail + "\n");
                }
            }
        }
    }
}
