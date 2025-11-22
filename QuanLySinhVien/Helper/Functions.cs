using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.Helper
{
    public class Functions
    {
        public static SqlConnection Conn;
        public static void Connect()
        {
            try
            {
                Conn = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString
                };
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                else
                {
                    MessageBox.Show("Kết nối thất bại!");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public static void Disconnect()
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //Kiểm tra khóa chính
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //Hàm thực hiện câu lệnh SQL (Insert, Update, Delete)
        public static void RunSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            cmd.Dispose();
            cmd = null;
        }
        

    }
}
