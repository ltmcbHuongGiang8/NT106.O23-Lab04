using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Lab4_ltmcb
{
    public partial class Bai4_Lab4 : Form
    {
        private readonly string websiteUrl = "https://betacinemas.vn/phim.htm";
        private List<Movie> movieList = new List<Movie>();

        public Bai4_Lab4()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            LoadMovies();
        }

        private void LoadMovies()
        {
            try
            {
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDoc = web.Load(websiteUrl);
                var h3Nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='film-info film-xs-info']/h3/a");
                var imgNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='pi-img-wrapper']/img[@src]");

                if (h3Nodes != null && imgNodes != null && h3Nodes.Count == imgNodes.Count)
                {
                    for (int i = 0; i < h3Nodes.Count; i++)
                    {
                        string tenPhim = h3Nodes[i].InnerText.Trim();
                        string imageUrl = imgNodes[i].GetAttributeValue("src", "");
                        string link = "https://betacinemas.vn/" + h3Nodes[i].GetAttributeValue("href", "");

                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Load(imageUrl);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                        AddMovie(tenPhim, imageUrl, link);
                    }
                }
                else
                {
                    MessageBox.Show("Không có thông tin về các bộ phim hoặc hình ảnh trên trang web.");
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải nội dung HTML từ URL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void AddMovie(string tenPhim, string imageUrl, string link)
        {
            ProgressBarWithLabel progressBarWithLabel = new ProgressBarWithLabel();
            progressBarWithLabel.SetLabelText(tenPhim);
            progressBarWithLabel.SetPictureBox(new PictureBox { ImageLocation = imageUrl, SizeMode = PictureBoxSizeMode.StretchImage });
            progressBarWithLabel.SetLabellinkText(link);
            progressBarWithLabel.ProgressBarClick += ProgressBarWithLabel_Click;

            flowLayoutPanel1.Controls.Add(progressBarWithLabel);

            // Add movie to the list
            movieList.Add(new Movie { TenPhim = tenPhim, ImageUrl = imageUrl, Link = link });
        }

        private void ProgressBarWithLabel_Click(object sender, EventArgs e)
        {
            ProgressBarWithLabel progressBarWithLabel = sender as ProgressBarWithLabel;
            string link = progressBarWithLabel.LinkLabelText;

            // Show detailed information of the movie
            Movie movie = movieList.Find(m => m.Link == link);
            if (movie != null)
            {
                MovieDetailsForm movieDetailsForm = new MovieDetailsForm(movie);
                movieDetailsForm.Show();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin chi tiết của bộ phim.");
            }
        }

        private void ExportDataToJson(List<Movie> movies)
        {
            string json = JsonConvert.SerializeObject(movies, Formatting.Indented);
            System.IO.File.WriteAllText(@"movies.json", json);
        }
    }

    public class Movie
    {
        public string TenPhim { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
    }

    public class MovieDetailsForm : Form
    {
        private Movie _movie;
        private WebBrowser _webBrowser;

        public MovieDetailsForm(Movie movie)
        {
            _movie = movie;

        //    InitializeComponent();

            // Set up UI elements
            Text = _movie.TenPhim;

            _webBrowser = new WebBrowser
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_webBrowser);

            _webBrowser.Navigate(_movie.Link);
        }
    }


}
