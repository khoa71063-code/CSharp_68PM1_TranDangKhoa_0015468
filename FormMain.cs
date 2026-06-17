using System;
using System.Data;
using System.Windows.Forms;

namespace Quanlisinhvien
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 1. Nạp dữ liệu lên bảng ngay khi mở ứng dụng
            HienThiDuLieu();

            // 2. Nạp dữ liệu mẫu cho ComboBox Giới tính (Nếu chưa có)
            if (cboGioiTinh.Items.Count == 0)
            {
                cboGioiTinh.Items.Add("Nam");
                cboGioiTinh.Items.Add("Nữ");
                cboGioiTinh.SelectedIndex = 0;
            }

            // 3. Nạp danh sách lớp vào ComboBox Lớp từ CSDL
            NapComboBoxLop();

            // 4. Đăng ký sự kiện Click cho các nút bấm cố định trên Form
            btnThem.Click += new EventHandler(btnThem_Click);
            btnSua.Click += new EventHandler(btnSua_Click);
            btnXoa.Click += new EventHandler(btnXoa_Click);
            btnLamMoi.Click += new EventHandler(btnLamMoi_Click);
            btnTim.Click += new EventHandler(btnTim_Click);
            dgvSinhVien.CellClick += new DataGridViewCellEventHandler(dgvSinhVien_CellClick);

            // Các nút Menu điều hướng phụ khác (nếu có nút đăng xuất)
            try
            {
                menuDangXuat.Click += (s, ev) => {
                    if (MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) this.Close();
                };
            }
            catch { }
        }

        // Hàm hiển thị dữ liệu từ CSDL lên DataGridView
        private void HienThiDuLieu()
        {
            try
            {
                string sql = "SELECT MaSV AS [Mã SV], HoTen AS [Họ và Tên], NgaySinh AS [Ngày Sinh], GioiTinh AS [Giới Tính], MaLop AS [Mã Lớp] FROM SinhVien";
                DataTable dt = KetNoi.GetDataTable(sql);
                dgvSinhVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách: " + ex.Message);
            }
        }

        // Hàm nạp danh sách lớp học tự động vào ComboBox cboLop
        private void NapComboBoxLop()
        {
            try
            {
                string sql = "SELECT DISTINCT MaLop FROM SinhVien";
                DataTable dt = KetNoi.GetDataTable(sql);
                cboLop.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    cboLop.Items.Add(row["MaLop"].ToString());
                }
                if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            }
            catch { }
        }

        // CHỨC NĂNG THÊM SINH VIÊN
        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam";
            string maLop = cboLop.Text.Trim();

            if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã Sinh Viên và Họ Tên!", "Thông báo");
                return;
            }

            string sql = $"INSERT INTO SinhVien(MaSV, HoTen, NgaySinh, GioiTinh, MaLop) VALUES('{maSV}', N'{hoTen}', '{ngaySinh}', N'{gioiTinh}', '{maLop}')";
            KetNoi.ThucThi(sql);

            MessageBox.Show("Thêm mới sinh viên thành công!", "Thông báo");
            HienThiDuLieu();
            XoaTrangForm();
            NapComboBoxLop();
        }

        // CHỨC NĂNG SỬA THÔNG TIN SINH VIÊN
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam";
            string maLop = cboLop.Text.Trim();

            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa từ bảng dữ liệu!", "Thông báo");
                return;
            }

            string sql = $"UPDATE SinhVien SET HoTen = N'{hoTen}', NgaySinh = '{ngaySinh}', GioiTinh = N'{gioiTinh}', MaLop = '{maLop}' WHERE MaSV = '{maSV}'";
            KetNoi.ThucThi(sql);

            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
            HienThiDuLieu();
            XoaTrangForm();
            NapComboBoxLop();
        }

        // CHỨC NĂNG XÓA SINH VIÊN
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa từ bảng dữ liệu!", "Thông báo");
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên mã {maSV} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string sql = $"DELETE FROM SinhVien WHERE MaSV = '{maSV}'";
                KetNoi.ThucThi(sql);

                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo");
                HienThiDuLieu();
                XoaTrangForm();
                NapComboBoxLop();
            }
        }

        // CHỨC NĂNG TÌM KIẾM SINH VIÊN
        private void btnTim_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                HienThiDuLieu();
                return;
            }

            string sql = $"SELECT MaSV AS [Mã SV], HoTen AS [Họ và Tên], NgaySinh AS [Ngày Sinh], GioiTinh AS [Giới Tính], MaLop AS [Mã Lớp] FROM SinhVien WHERE MaSV LIKE '%{tuKhoa}%' OR HoTen LIKE N'%{tuKhoa}%' OR MaLop LIKE '%{tuKhoa}%'";
            DataTable dt = KetNoi.GetDataTable(sql);
            dgvSinhVien.DataSource = dt;
        }

        // LÀM MỚI FORM NHẬP LIỆU
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrangForm();
            HienThiDuLieu();
        }

        private void XoaTrangForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtTimKiem.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            txtMaSV.Focus();
        }

        // SỰ KIỆN CLICK VÀO DÒNG TRÊN BẢNG ĐỂ ĐỔ DỮ LIỆU LÊN Ô CÓ SẴN
        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
                txtMaSV.Text = row.Cells[0].Value?.ToString();
                txtHoTen.Text = row.Cells[1].Value?.ToString();

                if (row.Cells[2].Value != DBNull.Value && row.Cells[2].Value != null)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells[2].Value);
                }

                string gt = row.Cells[3].Value?.ToString();
                if (cboGioiTinh.Items.Contains(gt)) cboGioiTinh.SelectedItem = gt;

                string lop = row.Cells[4].Value?.ToString();
                cboLop.Text = lop;
            }
        }

        // Sửa lỗi menu liên kết từ giao diện chính mà Visual Studio tìm kiếm
        private void menuSinhVien_Click(object sender, EventArgs e)
        {
            HienThiDuLieu();
        }
    }
}