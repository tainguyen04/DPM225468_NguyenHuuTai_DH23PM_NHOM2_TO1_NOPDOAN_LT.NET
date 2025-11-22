using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
            Conn = new SqlConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            if(Conn.State != System.Data.ConnectionState.Open)
            {
                Conn.Open();
            }
            else
            {
                MessageBox.Show("Kết nối thất bại!");
            }
        }
        public static void Disconnect()
        {
            if (Conn.State == System.Data.ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }
    }
}
