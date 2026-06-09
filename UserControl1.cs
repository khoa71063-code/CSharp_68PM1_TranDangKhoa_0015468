using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quanlisinhvien
{
    public partial class UserControl1 : UserControl
    {
        // Khởi tạo cấu trúc dữ liệu giả lập cho Lớp và Sinh Viên
        private List<string> danhSachLop = new List<string> { "68PM1", "68PM2", "68TH1", "68TH2" };
        private List<Student> danhSachSinhVien = new List<Student>();

        // Lớp đối tượng Sinh viên phục vụ lưu trữ
        public class Student
        {
            public string MaSV { get; set; }
            public string HoTen { get; set; }
            public string Lop { get; set; }
        }

        public UserControl1()
        {
            InitializeComponent();
        }

        // --- TỰ ĐỘNG TẢI DỮ LIỆU KHI LOAD USERCONTROL ---
        private void UserControl1_Load(object sender, EventArgs e)
        {
            // 1. Cấu hình hiển thị cho listView2 (Hiển thị dạng bảng chi tiết)
            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;

            // Thêm các cột cho listView2
            listView2.Columns.Add("Mã SV", 120);
            listView2.Columns.Add("Họ và Tên", 200);
            listView2.Columns.Add("Lớp học", 120);

            // Dữ liệu mẫu ban đầu cho danh sách sinh viên
            danhSachSinhVien.Add(new Student { MaSV = "0015468", HoTen = "Trần Đăng Khoa", Lop = "68PM1" });

            // 2. Gọi hàm hiển thị dữ liệu lên giao diện
            DisplayClassList4CBX();
            DisplayStudentList();
        }

        // --- TÍNH NĂNG 1: HIỂN THỊ DANH SÁCH LỚP LÊN COMBOBOX ---
        private void DisplayClassList4CBX()
        {
            // Xóa dữ liệu cũ nếu có
            comboBox1.Items.Clear();

            foreach (var lop in danhSachLop)
            {
                comboBox1.Items.Add(lop);
            }

            // Chọn mặc định phần tử đầu tiên nếu danh sách không trống
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        // --- TÍNH NĂNG 2: HIỂN THỊ DANH SÁCH SINH VIÊN LÊN LISTVIEW ---
        private void DisplayStudentList()
        {
            // Xóa các dòng cũ trên giao diện trước khi nạp mới
            listView2.Items.Clear();

            foreach (var sv in danhSachSinhVien)
            {
                ListViewItem item = new ListViewItem(sv.MaSV); // Cột đầu tiên: Mã SV
                item.SubItems.Add(sv.HoTen);                  // Cột thứ 2: Họ tên
                item.SubItems.Add(sv.Lop);                    // Cột thứ 3: Lớp

                listView2.Items.Add(item); // Thêm dòng vào listView2
            }
        }

        // --- TÍNH NĂNG 3: XỬ LÝ SỰ KIỆN THÊM_SINH_VIÊN (BUTTON 2) ---
        private void button2_Click(object sender, EventArgs e)
        {
            // Giả định: 
            // textBox2 là Mã Sinh Viên
            // textBox3 là Họ Tên Sinh Viên
            // comboBox1 là ComboBox chọn lớp học

            string maSV = textBox2.Text.Trim();
            string hoTen = textBox3.Text.Trim();

            // Lấy giá trị lớp đang được chọn trong ComboBox
            string lopHoc = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";

            // Kiểm tra ràng buộc dữ liệu đầu vào
            if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(lopHoc))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã SV, Họ tên và Chọn lớp học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng mã sinh viên trong danh sách
            if (danhSachSinhVien.Any(sv => sv.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sinh viên này đã tồn tại trên hệ thống!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tiến hành thêm sinh viên mới vào danh sách dữ liệu
            Student newStudent = new Student
            {
                MaSV = maSV,
                HoTen = hoTen,
                Lop = lopHoc
            };
            danhSachSinhVien.Add(newStudent);

            // Làm mới lại bảng hiển thị trên giao diện
            DisplayStudentList();

            // Xóa trắng các ô nhập liệu để sẵn sàng nhập tiếp
            textBox2.Clear();
            textBox3.Clear();
            textBox2.Focus();

            MessageBox.Show("Thêm sinh viên mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- CÁC HÀM SỰ KIỆN KHÁC GIỮ NGUYÊN THEO CODESMITH CỦA BẠN ---
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void pnlleft_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void button5_Click(object sender, EventArgs e) { }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button8_Click(object sender, EventArgs e) { }
        private void button10_Click(object sender, EventArgs e) { }
        private void button9_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void quảnLíLớpHọcToolStripMenuItem_Click(object sender, EventArgs e) { }
    }
}