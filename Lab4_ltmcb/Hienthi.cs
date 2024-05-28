using System;
using System.Windows.Forms;

namespace Lab4_ltmcb
{
    public partial class Hienthi : Form
    {
        private readonly string _url;

        public Hienthi(string url)
        {
            InitializeComponent();
            _url = url;
            LoadWebPage();
        }

        private void LoadWebPage()
        {
            webBrowser1.Navigate(_url);
        }
    }
}
