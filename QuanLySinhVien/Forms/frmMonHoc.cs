using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLySinhVien.Forms
{
    public partial class frmMonHoc : Form
    {
        DataTable tblMonHoc;
        string ma;
        public frmMonHoc()
        {
            InitializeComponent();
        }

        public void BatTat(bool giaTri)
        {
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;

            txtMaMonHoc.Enabled = giaTri;
            txtTenMonHoc.Enabled = giaTri;
            numTinChi.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            dgvMonHoc.AutoGenerateColumns = true;
            BatTat(false);
            LoadDataGridview();
            txtMaMonHoc.DataBindings.Clear();
            txtMaMonHoc.DataBindings.Add("Text", dgvMonHoc.DataSource, "MaMonHoc", false, DataSourceUpdateMode.Never);
            txtTenMonHoc.DataBindings.Clear();
            txtTenMonHoc.DataBindings.Add("Text", dgvMonHoc.DataSource, "TenMonHoc", false, DataSourceUpdateMode.Never);
            numTinChi.DataBindings.Clear();
            numTinChi.DataBindings.Add("Value", dgvMonHoc.DataSource, "SoTinChi", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaMonHoc.Enabled = true;
            BatTat(true);
            ma = "";
            txtMaMonHoc.Text = "";
            txtTenMonHoc.Text = "";
            numTinChi.Value = 1;
            txtMaMonHoc.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            if (dgvMonHoc.CurrentCell == null)
            {
                MessageBox.Show("Vui lòng chọn một Môn học để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvMonHoc.CurrentRow.Cells["MaMonHoc"].Value.ToString();
                txtMaMonHoc.Enabled = false;
                txtTenMonHoc.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaMonHoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMonHoc.Focus();
                return;
            }
            else if (txtTenMonHoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMonHoc.Focus();
                return;
            }
            else
            {
                string sql;
                if (string.IsNullOrEmpty(ma))
                {
                    sql = "SELECT MaMonHoc FROM tblMonHoc WHERE MaMonHoc = '" + txtMaMonHoc.Text.Trim() + "'";
                    if (Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã môn học đã tồn tại, bạn phải nhập mã môn học khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaMonHoc.Focus();
                        return;
                    }
                    sql = "INSERT INTO tblMonHoc(MaMonHoc, TenMonHoc, SoTinChi) VALUES('" + txtMaMonHoc.Text.Trim() + "',N'" + txtTenMonHoc.Text.Trim() + "'," + numTinChi.Value + ")";
                }
                else
                {
                    sql = "UPDATE tblMonHoc SET " +
                                     "TenMonHoc = N'" + txtTenMonHoc.Text.Trim() + "', " +
                                     "SoTinChi = " + numTinChi.Value + " " +
                                     "WHERE MaMonHoc = '" + ma + "'";
                }
                Helper.Functions.RunSQL(sql);
                frmMonHoc_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMonHoc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một môn học để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Kiểm tra môn học có trong bảng điểm không
                string maMonHoc = dgvMonHoc.CurrentRow.Cells["MaMonHoc"].Value.ToString();
                string checkSql = "SELECT MaMonHoc FROM tblDiemHocTap WHERE MaMonHoc = '" + maMonHoc + "'";
                if (Helper.Functions.CheckKey(checkSql))
                {
                    MessageBox.Show("Không thể xóa! Môn học này đã có điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "DELETE FROM tblMonHoc WHERE MaMonHoc = '" + maMonHoc + "'";
                    Helper.Functions.RunSQL(sql);
                    frmMonHoc_Load(sender, e);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            BatTat(false);
            frmMonHoc_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDataGridview()
        {
            string sql = "SELECT * FROM tblMonHoc";
            tblMonHoc = Helper.Functions.GetDataToTable(sql);
            dgvMonHoc.DataSource = tblMonHoc;
        }
    }
}