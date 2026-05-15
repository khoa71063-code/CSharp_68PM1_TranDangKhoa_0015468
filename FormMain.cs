using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quanlisinhvien
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            LoadDuLieuMau();
        }

        private void LoadDuLieuMau()
        {

            cboLop.Items.Add("68PM1 - Lớp 68PM1");
            cboLop.Items.Add("68PM2 - Lớp 68PM2");
            cboLop.SelectedIndex = 0; // Chọn mặc định dòng đầu tiên
            cboGioiTinh.SelectedIndex = 0; // Chọn mặc định là Nam

            // 2. Tạo bảng cấu trúc DataGridView giống y như ảnh
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã SV", typeof(string));
            dt.Columns.Add("Họ và Tên", typeof(string));
            dt.Columns.Add("Giới Tính", typeof(string));
            dt.Columns.Add("Ngày Sinh", typeof(string));
            dt.Columns.Add("Lớp", typeof(string));

            // 3. Đổ 3 dòng dữ liệu mẫu chuẩn theo ảnh của bạn
            dt.Rows.Add("1", "hieu", "Nam", "11/03/2026", "68PM1");
            dt.Rows.Add("2", "Nguyễn Văn B", "Nam", "11/03/2026", "68PM2");
            dt.Rows.Add("3", "Trần Văn C", "Nam", "21/03/2026", "68PM2");

            // Gán dữ liệu vào DataGridView
            dgvSinhVien.DataSource = dt;
        }
    }
}