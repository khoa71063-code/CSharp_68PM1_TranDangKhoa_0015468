using System;
using System.Data;
using System.Data.SqlClient;

namespace Quanlisinhvien
{
    class KetNoi
    {
        // Đã sửa chuẩn Data Source theo tên Server máy của Khoa
        // Hãy đảm bảo "Initial Catalog" khớp chính xác với tên Database bạn tạo trong SQL Server
        public static string strConn = @"Data Source=DESKTOP-RLLTPER\SQLEXPRESS;Initial Catalog=QuanLiSinhVienDB;Integrated Security=True;TrustServerCertificate=True;";

        public static DataTable GetDataTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi Hệ Thống");
                    }
                    return dt;
                }
            }
        }

        public static void ThucThi(string sql)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Lỗi thực thi SQL: " + ex.Message, "Lỗi Hệ Thống");
                    }
                }
            }
        }
    }
}