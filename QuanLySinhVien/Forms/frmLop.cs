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
    public partial class frmLop: Form
    {
        DataTable tblLop;
        string ma;
        public frmLop()
        {
            InitializeComponent();
        }
        public void BatTat(bool giaTri)
        {
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
            

            txtMaLop.Enabled = giaTri;
            txtTenLop.Enabled = giaTri;
            cboTenKhoa.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
            if (btnSua.Enabled == true)
                txtMaLop.Enabled = giaTri;

        }
        private void LayDanhSachKhoa()
        {
            string sql = "SELECT * FROM tblKhoa";
            DataTable tblKhoa = Helper.Functions.GetDataToTable(sql);
            cboTenKhoa.DataSource = tblKhoa;
            cboTenKhoa.DisplayMember = "TenKhoa";
            cboTenKhoa.ValueMember = "MaKhoa";
        }
        private void frmLop_Load(object sender, EventArgs e)
        {
            dgvLop.AutoGenerateColumns = false;
            LoadDataToGridview();
            LayDanhSachKhoa();
            BatTat(false);
            txtMaLop.DataBindings.Clear();
            txtMaLop.DataBindings.Add("Text", dgvLop.DataSource, "MaLop", false, DataSourceUpdateMode.Never);
            txtTenLop.DataBindings.Clear();
            txtTenLop.DataBindings.Add("Text", dgvLop.DataSource, "TenLop", false, DataSourceUpdateMode.Never);
            cboTenKhoa.DataBindings.Clear();
            cboTenKhoa.DataBindings.Add("SelectedValue", dgvLop.DataSource, "MaKhoa", false, DataSourceUpdateMode.Never);
            numSiSo.DataBindings.Clear();
            numSiSo.DataBindings.Add("Value",dgvLop.DataSource,"SiSo",false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaLop.Enabled = true;
            BatTat(true);
            ma = "";
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            cboTenKhoa.Text = "";
            numSiSo.Value = 1;
            txtMaLop.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            txtMaLop.Enabled = false;
            if(dgvLop.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvLop.CurrentRow.Cells["MaLop"].Value.ToString();
                txtTenLop.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtMaLop.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLop.Focus();
                return;
            }
            else if(txtTenLop.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus();
                return;
            }
            else if(cboTenKhoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenKhoa.Focus();
                return;
            }else if(numSiSo.Value == 0)
            {
                MessageBox.Show("Bạn phải nhập sỉ số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSiSo.Focus();
                return;
            }
            else
            {
                string sql;
                int siSo = (int)numSiSo.Value;
                if (string.IsNullOrEmpty(ma))
                {
                    sql = "SELECT MaLop FROM tblLop WHERE MaLop = '" + txtMaLop.Text.Trim() + "'";
                    if (Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã lớp đã tồn tại, bạn phải nhập mã lớp khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaLop.Focus();
                        return;
                    }
                    sql = "INSERT INTO tblLop(MaLop, TenLop, SiSo, MaKhoa) " +
                                 "VALUES('" + txtMaLop.Text.Trim() + "',N'" + txtTenLop.Text.Trim() + "'," +
                                 siSo + ",'" + cboTenKhoa.SelectedValue.ToString() + "')";
                }
                else
                {
                    sql = "UPDATE tblLop SET " +
                        "TenLop = N'" + txtTenLop.Text.Trim() + "', " +
                        "SiSo = " + siSo + ", " +
                        "MaKhoa = '" + cboTenKhoa.SelectedValue.ToString() + "' " +
                        "WHERE MaLop = '" + ma + "'";
                }
                Helper.Functions.RunSQL(sql);
                frmLop_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dgvLop.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa lớp này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maLop = dgvLop.CurrentRow.Cells["MaLop"].Value.ToString();
                    string sql = "DELETE FROM tblLop WHERE MaLop = '" + maLop + "'";
                    Helper.Functions.RunSQL(sql);
                    frmLop_Load(sender, e);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadDataToGridview()
        {
            string sql = @"SELECT tblLop.MaLop, tblLop.TenLop, tblLop.MaKhoa, tblLop.SiSo, 
                          tblKhoa.TenKhoa 
                   FROM tblLop 
                   INNER JOIN tblKhoa ON tblLop.MaKhoa = tblKhoa.MaKhoa";
            tblLop = Helper.Functions.GetDataToTable(sql);
            dgvLop.DataSource = tblLop;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"SELECT tblLop.MaLop, tblLop.TenLop, tblLop.MaKhoa, tblLop.SiSo, tblKhoa.TenKhoa
                   FROM tblLop
                   INNER JOIN tblKhoa ON tblLop.MaKhoa = tblKhoa.MaKhoa
                   WHERE tblLop.MaLop LIKE N'%" + key + "%' OR tblLop.TenLop LIKE N'%" + key + "%'";

            tblLop = Helper.Functions.GetDataToTable(sql);

            if (tblLop.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblLop.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvLop.DataSource = tblLop;
            txtTimKiem.Clear();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmLop_Load(sender, e);
        }

        
    }
}
