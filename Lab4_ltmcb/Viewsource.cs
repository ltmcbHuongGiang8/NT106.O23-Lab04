using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_ltmcb
{
    public partial class Viewsource : Form
    {
        public Viewsource()
        {
            InitializeComponent();
        }
        public void Hienthi(string s)
        {
            richTextBox1.Text = s;
        }
    }
}
