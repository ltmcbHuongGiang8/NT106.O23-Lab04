using HtmlAgilityPack;
using Lab4_ltmcb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab4_ltmcb
{
    public partial class Bai3_Lab4 : Form
    {
        public Bai3_Lab4()
        {
            InitializeComponent();
        }

      
        private string getHTML(string szURL)
        {
            WebRequest request = WebRequest.Create(szURL);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            response.Close();
            return responseFromServer;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var html = getHTML(textBox1.Text);
            Viewsource view = new Viewsource();
            view.Hienthi(html.ToString());
            view.Show();
        }

        private void DownloadImages(List<string> imageUrls, string destinationDirectory)
        {
            foreach (var imageUrl in imageUrls)
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        string fileName = imageUrl.Substring(imageUrl.LastIndexOf("/") + 1);
                        string path = Path.Combine(destinationDirectory, fileName);
                        client.DownloadFile(imageUrl, path);
                        MessageBox.Show("Image downloaded successfully at: " + path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error downloading image: " + ex.Message);
                    }
                }
            }
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            webView21.Source = new Uri(url);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter a valid URL.");
                return;
            }

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(textBox1.Text);

            if (htmlDoc == null)
            {
                MessageBox.Show("Failed to load HTML document.");
                return;
            }

            var imgs = htmlDoc.DocumentNode.SelectNodes("//img");
            if (imgs == null)
            {
                MessageBox.Show("No image found in the HTML document.");
                return;
            }

            List<string> imageUrls = new List<string>();

            foreach (var img in imgs)
            {
                var imgURL = img.GetAttributeValue("src", "");
                if (!string.IsNullOrEmpty(imgURL))
                {
                    imageUrls.Add(imgURL);
                }
            }

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                DownloadImages(imageUrls, folderBrowserDialog.SelectedPath);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
