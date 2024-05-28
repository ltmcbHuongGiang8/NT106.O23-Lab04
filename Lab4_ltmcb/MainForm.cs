
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bai1_Lab4 bai1_Lab4 = new Bai1_Lab4();
            bai1_Lab4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bai2_Lab4 bai2_Lab4 = new Bai2_Lab4();
            bai2_Lab4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bai3_Lab4 bai3_Lab4 = new Bai3_Lab4();  
            bai3_Lab4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
         Bai4_Lab4 bai4_Lab4 = new Bai4_Lab4(); 
            bai4_Lab4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bai5_Lab4 bai5_Lab4 = new Bai5_Lab4();
            bai5_Lab4.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bai6_Lab4 bai6_Lab4 = new Bai6_Lab4();  
            bai6_Lab4.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bai7_Lab4 bai7_Lab4 = new Bai7_Lab4();
            bai7_Lab4.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
