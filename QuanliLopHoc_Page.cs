using System;
using System.Data;
using System.Windows.Forms;

namespace Quanlisinhvien
{
    public partial class QuanliLopHoc_Page : UserControl
    {
        public QuanliLopHoc_Page()
        {
            InitializeComponent();
            this.Load += new EventHandler(QuanliLopHoc_Page_Load);
        }

        private void QuanliLopHoc_Page_Load(object sender, EventArgs e)
        {
            HienThiDuLieuLop();
            GanSuKienNutLopTuDong();
        }

        private void HienThiDuLieuLop()
        {
            try
            {
                string sql = "SELECT MaLop AS [Mã Lớp], TenLop AS [Tên Lớp], KhoaHoc AS [Khóa Học] FROM LopHoc";
                DataTable dt = KetNoi.GetDataTable(sql);
                QuetDataGridLop(this, dt);
            }
            catch { }
        }

        private void QuetDataGridLop(Control parent, DataTable dt)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.DataSource = dt;
                    dgv.CellClick -= DataGridViewLop_CellClick;
                    dgv.CellClick += DataGridViewLop_CellClick;
                    return;
                }
                if (c.HasChildren) QuetDataGridLop(c, dt);
            }
        }

        private void XuLyThemLop(object sender, EventArgs e)
        {
            try
            {
                string maLop = LayTxtLop(this, 1);
                string tenLop = LayTxtLop(this, 2);
                string khoaHoc = LayTxtLop(this, 3);

                if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(tenLop)) return;

                string sql = $"INSERT INTO LopHoc VALUES('{maLop}', N'{tenLop}', N'{khoaHoc}')";
                KetNoi.GetDataTable(sql);
                MessageBox.Show("Thêm lớp học thành công!");
                HienThiDuLieuLop();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void XuLySuaLop(object sender, EventArgs e)
        {
            try
            {
                string maLop = LayTxtLop(this, 1);
                string tenLop = LayTxtLop(this, 2);
                string khoaHoc = LayTxtLop(this, 3);

                string sql = $"UPDATE LopHoc SET TenLop=N'{tenLop}', KhoaHoc=N'{khoaHoc}' WHERE MaLop='{maLop}'";
                KetNoi.GetDataTable(sql);
                HienThiDuLieuLop();
            }
            catch { }
        }

        private void XuLyXoaLop(object sender, EventArgs e)
        {
            try
            {
                string maLop = LayTxtLop(this, 1);
                string sql = $"DELETE FROM LopHoc WHERE MaLop='{maLop}'";
                KetNoi.GetDataTable(sql);
                HienThiDuLieuLop();
            }
            catch { }
        }

        private string LayTxtLop(Control parent, int target)
        {
            int count = 0;
            return TimTxtLop(parent, target, ref count);
        }

        private string TimTxtLop(Control parent, int target, ref int count)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    count++;
                    if (count == target) return txt.Text.Trim();
                }
                if (c.HasChildren)
                {
                    string res = TimTxtLop(c, target, ref count);
                    if (!string.IsNullOrEmpty(res)) return res;
                }
            }
            return "";
        }

        private void DataGridViewLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && sender is DataGridView dgv)
            {
                try
                {
                    DataGridViewRow row = dgv.Rows[e.RowIndex];
                    int count = 0;
                    DienTxtLop(this, row, ref count);
                }
                catch { }
            }
        }

        private void DienTxtLop(Control parent, DataGridViewRow row, ref int count)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    if (count < row.Cells.Count && row.Cells[count].Value != null)
                        txt.Text = row.Cells[count].Value.ToString();
                    count++;
                }
                if (c.HasChildren) DienTxtLop(c, row, ref count);
            }
        }

        private void GanSuKienNutLopTuDong()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button btn)
                {
                    if (btn.Text.Contains("Thêm")) btn.Click += XuLyThemLop;
                    if (btn.Text.Contains("Sửa")) btn.Click += XuLySuaLop;
                    if (btn.Text.Contains("Xóa")) btn.Click += XuLyXoaLop;
                }
            }
        }
    }
}