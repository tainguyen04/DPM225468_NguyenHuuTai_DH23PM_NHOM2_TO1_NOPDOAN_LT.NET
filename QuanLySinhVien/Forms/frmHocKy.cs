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
    public partial class frmHocKy: Form
    {
        DataTable tblHocKy;
        string ma;
        public frmHocKy()
        {
            InitializeComponent();
        }
        public void BatTat(bool giaTri)
        {
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
           

            txtMaHocKy.Enabled = giaTri;
            txtTenHocKy.Enabled = giaTri;
            
            btnLuu.Enabled = giaTri;

        }

        private void frmHocKy_Load(object sender, EventArgs e)
        {
            dgvHocKy.AutoGenerateColumns = false;
            BatTat(false);
            LoadDataGridview();
            txtMaHocKy.DataBindings.Clear();
            txtMaHocKy.DataBindings.Add("Text", dgvHocKy.DataSource, "MaHocKy", false, DataSourceUpdateMode.Never);
            txtTenHocKy.DataBindings.Clear();
            txtTenHocKy.DataBindings.Add("Text", dgvHocKy.DataSource, "TenHocKy", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaHocKy.Enabled = true;
            BatTat(true);
            ma = "";
            txtMaHocKy.Text = "";
            txtTenHocKy.Text = "";
            txtMaHocKy.Focus();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            if(dgvHocKy.CurrentCell == null)
            {
                MessageBox.Show("Vui lòng chọn một Học kỳ để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvHocKy.CurrentRow.Cells["MaHocKy"].Value.ToString();
                txtMaHocKy.Enabled = false;
                txtTenHocKy.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaHocKy.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHocKy.Focus();
                return;
            }
            else if (txtTenHocKy.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHocKy.Focus();
                return;
            }
            else
            {
                string sql;
                if (string.IsNullOrEmpty(ma))
                {
                    sql = "SELECT MaHocKy FROM tblHocKy WHERE MaHocKy = '" + txtMaHocKy.Text.Trim() + "'";
                    if (Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã học kỳ đã tồn tại, bạn phải nhập mã học kỳ khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaHocKy.Focus();
                        return;
                    }
                    sql = "INSERT INTO tblHocKy(MaHocKy, TenHocKy) VALUES('" + txtMaHocKy.Text.Trim() + "',N'" + txtTenHocKy.Text.Trim() + "')";

                }
                else
                {
                    sql = "UPDATE tblHocKy SET TenHocKy = N'" + txtTenHocKy.Text.Trim() + "' WHERE MaHocKy = '" + ma + "'";
                }
                Helper.Functions.RunSQL(sql);
                frmHocKy_Load(sender, e);
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHocKy.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một học kỳ để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa học kỳ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maHocKy = dgvHocKy.CurrentRow.Cells["MaHocKy"].Value.ToString();
                    string sql = "DELETE FROM tblHocKy WHERE MaHocKy=N'" + maHocKy + "'";
                    Helper.Functions.RunSQL(sql);
                    frmHocKy_Load(sender, e);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadDataGridview()
        {
            string sql = "SELECT * FROM tblHocKy";
            tblHocKy = Helper.Functions.GetDataToTable(sql);
            dgvHocKy.DataSource = tblHocKy;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "SELECT * FROM tblHocKy WHERE MaHocKy LIKE N'%" + key + "%' OR TenHocKy LIKE N'%" + key + "%'";

            tblHocKy = Helper.Functions.GetDataToTable(sql);

            if (tblHocKy.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblHocKy.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvHocKy.DataSource = tblHocKy;
            txtTimKiem.Clear();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmHocKy_Load(sender, e);
        }

        
    }
}
