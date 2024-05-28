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

namespace Lab4_ltmcb
{
    public partial class Bai6_Lab4 : Form
    {
        private readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(@"https://nt106.uitiot.vn")
        };

        public Bai6_Lab4()
        {
            InitializeComponent();
        }

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
                        string accessToken = jsonResponse["access_token"].ToString();
                        HandleSuccessfulLogin(accessToken);
                    }
                }
                else
                {
                    await HandleUnsuccessfulLogin(response);
                }
            }
        }

        private async Task HandleUnsuccessfulLogin(HttpResponseMessage response)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            JObject errorJson = JObject.Parse(errorResponse);
            string detail = errorJson["detail"].ToString();
            richTextBox1.AppendText($"Lỗi: {detail}\n");
        }

        private async Task HandleSuccessfulLogin(string accessToken)
        {
            // Hiển thị thông báo đăng nhập thành công
            richTextBox1.AppendText("Đăng nhập thành công\n");

            // Thực hiện yêu cầu GET thông tin người dùng
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage userInfoResponse = await httpClient.GetAsync("api/v1/user/me");

            using (userInfoResponse)
            {
                if (userInfoResponse.IsSuccessStatusCode)
                {
                    var userInfoRes = await userInfoResponse.Content.ReadAsStringAsync();
                    JObject userInfoJson = JObject.Parse(userInfoRes);

                    richTextBox1.AppendText("Thông tin người dùng:\n");
                    richTextBox1.AppendText($"ID: {userInfoJson["id"]}\n");
                    richTextBox1.AppendText($"Username: {userInfoJson["username"]}\n");
                    richTextBox1.AppendText($"Email: {userInfoJson["email"]}\n");

                    // Hiển thị các thông tin cơ bản khác của người dùng
                    // Ví dụ: Hiển thị tên và địa chỉ nếu có
                    if (userInfoJson["name"] != null)
                        richTextBox1.AppendText($"Tên: {userInfoJson["name"]}\n");
                    if (userInfoJson["address"] != null)
                        richTextBox1.AppendText($"Địa chỉ: {userInfoJson["address"]}\n");
                }
                else
                {
                    await HandleUnsuccessfulUserInfoResponse(userInfoResponse);
                }
            }
        }

        private async Task HandleUnsuccessfulUserInfoResponse(HttpResponseMessage userInfoResponse)
        {
            var errorResponse = await userInfoResponse.Content.ReadAsStringAsync();
            JObject errorJson = JObject.Parse(errorResponse);
            string detail = errorJson["detail"].ToString();
            richTextBox1.AppendText($"Lỗi: {detail}\n");
        }
    }
}
