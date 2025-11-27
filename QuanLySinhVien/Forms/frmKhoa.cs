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
    public partial class frmKhoa: Form
    {
        DataTable tblKhoa;
        string ma; 
        public frmKhoa()
        {
            InitializeComponent();
        }
        public void BatTat(bool giaTri)
        {
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
           

            txtMaKhoa.Enabled = giaTri;
            txtTenKhoa.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
            if (btnSua.Enabled == true)
                txtMaKhoa.Enabled = giaTri;

        }
        private void frmKhoa_Load(object sender, EventArgs e)
        {
            dgvKhoa.AutoGenerateColumns = false;
            BatTat(false);
            LoadDataToGridview();
            txtMaKhoa.DataBindings.Clear();
            txtMaKhoa.DataBindings.Add("Text", dgvKhoa.DataSource, "MaKhoa",false,DataSourceUpdateMode.Never);
            txtTenKhoa.DataBindings.Clear();
            txtTenKhoa.DataBindings.Add("Text", dgvKhoa.DataSource, "TenKhoa", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Enabled = true;
            BatTat(true);
            ma = "";
            txtMaKhoa.Text = "";
            txtTenKhoa.Text = "";
            txtMaKhoa.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            if (dgvKhoa.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một khoa để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvKhoa.CurrentRow.Cells["MaKhoa"].Value.ToString();
                txtMaKhoa.Enabled = false;
                txtTenKhoa.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtMaKhoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKhoa.Focus();
                return;
            }
            else if (txtTenKhoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhoa.Focus();
                return;
            }
            else
            {
                string sql;
                if (string.IsNullOrEmpty(ma))
                {
                    sql = "SELECT MaKhoa FROM tblKhoa WHERE MaKhoa=N'" + txtMaKhoa.Text.Trim() + "'";
                    if (Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã khoa đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaKhoa.Focus();
                        return;
                    }
                    sql = "INSERT INTO tblKhoa(MaKhoa, TenKhoa) VALUES(N'" + txtMaKhoa.Text.Trim() + "',N'" + txtTenKhoa.Text.Trim() + "')";

                }
                else
                {
                    sql = "UPDATE tblKhoa SET TenKhoa=N'" + txtTenKhoa.Text.Trim() + "' WHERE MaKhoa=N'" + txtMaKhoa.Text.Trim() + "'";
                }
                Helper.Functions.RunSQL(sql);
                frmKhoa_Load(sender, e);
            }
                
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dgvKhoa.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một khoa để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {   
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa khoa này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maKhoa = dgvKhoa.CurrentRow.Cells["MaKhoa"].Value.ToString();
                    string sql = "DELETE FROM tblKhoa WHERE MaKhoa=N'" + maKhoa + "'";
                    Helper.Functions.RunSQL(sql);
                    frmKhoa_Load(sender, e);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadDataToGridview()
        {
            string sql = "SELECT * FROM tblKhoa";
            tblKhoa = Helper.Functions.GetDataToTable(sql);
            dgvKhoa.DataSource = tblKhoa;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "SELECT * FROM tblKhoa WHERE MaKhoa LIKE N'%" + key + "%' OR TenKhoa LIKE N'%" + key + "%'";

            tblKhoa = Helper.Functions.GetDataToTable(sql);

            if (tblKhoa.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblKhoa.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvKhoa.DataSource = tblKhoa;
            txtTimKiem.Clear();

        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmKhoa_Load(sender, e);
        }

        
    }
}
