using BCrypt.Net;
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
    public partial class frmTaiKhoan: Form
    {
        DataTable tblTaiKhoan;
        string ma;
        public frmTaiKhoan()
        {
            InitializeComponent();
        }
        public void BatTat(bool giaTri)
        {
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;

            txtTenDangNhap.Enabled = giaTri;
            txtMatKhau.Enabled = giaTri;
            cboQuyen.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            BatTat(false);
            LoadDataGridview();
            txtTenDangNhap.DataBindings.Clear();
            txtTenDangNhap.DataBindings.Add("Text", dgvTaiKhoan.DataSource, "TenDangNhap", false, DataSourceUpdateMode.Never);

            cboQuyen.DataBindings.Clear();
            cboQuyen.DataBindings.Add("Text", dgvTaiKhoan.DataSource, "Quyen", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            BatTat(true);
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            cboQuyen.SelectedIndex = -1;
            txtTenDangNhap.Enabled = true;
            txtTenDangNhap.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            if (dgvTaiKhoan.CurrentCell == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvTaiKhoan.CurrentRow.Cells["TenDangNhap"].Value.ToString();
                txtTenDangNhap.Enabled = false;
                txtMatKhau.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Nhập tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (cboQuyen.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn quyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboQuyen.Focus();
                return;
            }

            string tenDN = txtTenDangNhap.Text.Trim();
            string quyen = cboQuyen.Text;
            string matKhau = txtMatKhau.Text.Trim();

            string sql;

            if (string.IsNullOrEmpty(ma)) // Thêm
            {
                // Kiểm tra tồn tại
                sql = "SELECT TenDangNhap FROM tblTaiKhoan WHERE TenDangNhap='" + tenDN + "'";
                if (Helper.Functions.CheckKey(sql))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    txtTenDangNhap.Focus();
                    return;
                }

                // Nếu không nhập mật khẩu → đặt mặc định
                if (string.IsNullOrWhiteSpace(matKhau))
                    matKhau = "123456";

                string matKhauHashed = BCrypt.Net.BCrypt.HashPassword(matKhau);
                // Có thể hash mật khẩu ở đây nếu muốn
                sql = "INSERT INTO tblTaiKhoan(TenDangNhap, MatKhau, Quyen) " +
                      "VALUES('" + tenDN + "', '" + matKhauHashed + "', '" + quyen + "')";
            }
            else // Sửa
            {
                // Lấy mật khẩu cũ
                object result = Helper.Functions.GetFieldValues("SELECT MatKhau FROM tblTaiKhoan WHERE TenDangNhap='" + tenDN + "'");
                string oldMatKhau = result?.ToString() ?? "";

                // Nếu người dùng không nhập mật khẩu mới → giữ cũ
                if (string.IsNullOrWhiteSpace(matKhau))
                    matKhau = oldMatKhau;
                else
                    matKhau = BCrypt.Net.BCrypt.HashPassword(matKhau); // băm mật khẩu mới

                sql = "UPDATE tblTaiKhoan SET MatKhau='" + matKhau + "', Quyen='" + quyen + "' " +
                      "WHERE TenDangNhap='" + tenDN + "'";
            }
            Helper.Functions.RunSQL(sql);
           frmTaiKhoan_Load(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow != null)
            {
                string tenDN = dgvTaiKhoan.CurrentRow.Cells["TenDangNhap"].Value.ToString();
                if (MessageBox.Show("Xác nhận xóa tài khoản " + tenDN + "?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sql = "DELETE FROM tblTaiKhoan WHERE TenDangNhap='" + tenDN + "'";
                    Helper.Functions.RunSQL(sql);
                    LoadDataGridview();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void LoadDataGridview()
        {
            string sql = "SELECT * FROM tblTaiKhoan";
            tblTaiKhoan = Helper.Functions.GetDataToTable(sql);
            dgvTaiKhoan.DataSource = tblTaiKhoan;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenDN = txtTimKiem.Text.Trim();

            // Nếu chưa nhập gì
            if (string.IsNullOrEmpty(tenDN))
            {
                MessageBox.Show("Bạn hãy nhập Tên đăng nhập để tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu SQL
            string sql = "SELECT * FROM tblTaiKhoan WHERE TenDangNhap LIKE N'%" + tenDN + "%'";

            // Lấy dữ liệu
            tblTaiKhoan = Helper.Functions.GetDataToTable(sql);

            // Kiểm tra kết quả
            if (tblTaiKhoan.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblTaiKhoan.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Hiển thị lên DataGridView
            dgvTaiKhoan.DataSource = tblTaiKhoan;
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmTaiKhoan_Load(sender, e);
        }
    }
}
