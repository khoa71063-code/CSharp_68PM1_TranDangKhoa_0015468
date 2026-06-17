using System;
using System.Windows.Forms;

namespace Quanlisinhvien
{
    public partial class UserControl1 : UserControl
    {
        private int currentPage = 1;

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            // Để trống hoặc xử lý nhãn phân trang
        }

        public void button7_Click(object sender, EventArgs e) { currentPage = 1; }
        public void button8_Click(object sender, EventArgs e) { if (currentPage > 1) currentPage--; }
        public void button9_Click(object sender, EventArgs e) { currentPage++; }
        public void button10_Click(object sender, EventArgs e) { currentPage = 2; }
        public void quảnLíLớpHọcToolStripMenuItem_Click(object sender, EventArgs e) { }
        public void pnlleft_Paint(object sender, PaintEventArgs e) { }
        public void groupBox1_Enter(object sender, EventArgs e) { }
        public void label1_Click(object sender, EventArgs e) { }
        public void label2_Click(object sender, EventArgs e) { }
        public void label3_Click(object sender, EventArgs e) { }
        public void label4_Click(object sender, EventArgs e) { }
        public void textBox2_TextChanged(object sender, EventArgs e) { }
        public void textBox3_TextChanged(object sender, EventArgs e) { }
        public void textBox4_TextChanged(object sender, EventArgs e) { }
        public void button2_Click(object sender, EventArgs e) { }
        public void button5_Click(object sender, EventArgs e) { }
        public void listView2_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}