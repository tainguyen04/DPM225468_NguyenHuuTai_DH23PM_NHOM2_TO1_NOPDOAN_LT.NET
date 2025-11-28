    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace QuanLySinhVien.Forms
    {
        public partial class frmDangNhap: Form
        {
            public string Quyen;
            public frmDangNhap()
            {
                InitializeComponent();
            }

            private void frmDangNhap_Load(object sender, EventArgs e)
            {

            }

            private void btnDangNhap_Click(object sender, EventArgs e)
            {
                string tenDangNhap = txtTenDangNhap.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();
                object matKhauHashed = Helper.Functions.GetFieldValues($"SELECT MatKhau FROM tblTaiKhoan WHERE TenDangNhap='{tenDangNhap}'");

                string hashed = matKhauHashed?.ToString();

                object result = Helper.Functions.GetFieldValues($"SELECT Quyen FROM tblTaiKhoan WHERE TenDangNhap='{tenDangNhap}'");

                string quyen = result?.ToString().Trim();
            if (string.IsNullOrEmpty(hashed))
                {
                    MessageBox.Show("Tên đăng nhập không tồn tại!");
                    return;
                }
                if (BCrypt.Net.BCrypt.Verify(matKhau, hashed))
                {
                    this.Quyen = quyen; 
                    MessageBox.Show("Đăng nhập thành công với quyền:"+quyen);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu hoặc tên đăng nhập!");
                }
            }

        private void chkMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu checkbox được tick thì hiện mật khẩu (bỏ dấu chấm)
            if (chkMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = '\0'; // '\0' = không ẩn ký tự
            }
            else
            {
                txtMatKhau.PasswordChar = '•'; // hoặc '*'
            }
        }
    }
    }
