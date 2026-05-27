using System;
using System.Windows.Forms;

namespace Quanlisinhvien
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (user.Equals("0015468@st.huce.edu.vn") && pass.Equals("0015468"))
            {
                MessageBox.Show("Đăng nhập hệ thống thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormMain fm = new FormMain();
                fm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.", "Lỗi đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

    }
}