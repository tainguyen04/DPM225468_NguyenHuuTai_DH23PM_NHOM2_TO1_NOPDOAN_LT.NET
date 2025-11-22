using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLySinhVien.Forms
{
    public partial class frmDiemHocTap : Form
    {
        DataTable tblDiemHocTap;
        string ma;
        public frmDiemHocTap()
        {
            InitializeComponent();
        }

        public void BatTat(bool giaTri)
        {
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;

            cboMSSV.Enabled = giaTri;
            cboTenHocKy.Enabled = giaTri;
            cboTenMonHoc.Enabled = giaTri;
            numHe10.Enabled = giaTri;
            numHe4.Enabled = giaTri;
            cboXepLoai.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
        }

        private void LayDanhSachSinhVien()
        {
            string sql = "SELECT * FROM tblSinhVien";
            DataTable tblSinhVien = Helper.Functions.GetDataToTable(sql);
            cboMSSV.DataSource = tblSinhVien;
            cboMSSV.DisplayMember = "MSSV";
            cboMSSV.ValueMember = "MSSV";
        }

        private void LayDanhSachMonHoc()
        {
            string sql = "SELECT * FROM tblMonHoc";
            DataTable tblMonHoc = Helper.Functions.GetDataToTable(sql);
            cboTenMonHoc.DataSource = tblMonHoc;
            cboTenMonHoc.DisplayMember = "TenMonHoc";
            cboTenMonHoc.ValueMember = "MaMonHoc";
        }

        private void LayDanhSachHocKy()
        {
            string sql = "SELECT * FROM tblHocKy";
            DataTable tblHocKy = Helper.Functions.GetDataToTable(sql);
            cboTenHocKy.DataSource = tblHocKy;
            cboTenHocKy.DisplayMember = "TenHocKy";
            cboTenHocKy.ValueMember = "MaHocKy";
        }

        private void LoadXepLoai()
        {
            cboXepLoai.Items.Clear();
            cboXepLoai.Items.Add("Giỏi");
            cboXepLoai.Items.Add("Khá");
            cboXepLoai.Items.Add("Trung bình");
            cboXepLoai.Items.Add("Yếu");
        }

        private void frmDiemHocTap_Load(object sender, EventArgs e)
        {
            dgvDiemHocTap.AutoGenerateColumns = true;
            BatTat(false);
            LayDanhSachSinhVien();
            LayDanhSachMonHoc();
            LayDanhSachHocKy();
            LoadXepLoai();
            LoadDataGridview();

            // Data binding
            cboMSSV.DataBindings.Clear();
            cboMSSV.DataBindings.Add("Text", dgvDiemHocTap.DataSource, "MSSV", false, DataSourceUpdateMode.Never);
            cboTenHocKy.DataBindings.Clear();
            cboTenHocKy.DataBindings.Add("SelectedValue", dgvDiemHocTap.DataSource, "MaHocKy", false, DataSourceUpdateMode.Never);
            cboTenMonHoc.DataBindings.Clear();
            cboTenMonHoc.DataBindings.Add("SelectedValue", dgvDiemHocTap.DataSource, "MaMonHoc", false, DataSourceUpdateMode.Never);
            numHe10.DataBindings.Clear();
            numHe10.DataBindings.Add("Value", dgvDiemHocTap.DataSource, "DiemHe10", false, DataSourceUpdateMode.Never);
            numHe4.DataBindings.Clear();
            numHe4.DataBindings.Add("Value", dgvDiemHocTap.DataSource, "DiemHe4", false, DataSourceUpdateMode.Never);
            cboXepLoai.DataBindings.Clear();
            cboXepLoai.DataBindings.Add("Text", dgvDiemHocTap.DataSource, "XepLoai", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            BatTat(true);
            ma = "";
            cboMSSV.Text = "";
            cboTenHocKy.Text = "";
            cboTenMonHoc.Text = "";
            numHe10.Value = 0;
            numHe4.Value = 0;
            cboXepLoai.Text = "";
            cboMSSV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            if (dgvDiemHocTap.CurrentCell == null)
            {
                MessageBox.Show("Vui lòng chọn một Điểm để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvDiemHocTap.CurrentRow.Cells["Id"].Value.ToString();
                cboMSSV.Focus();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMSSV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn MSSV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMSSV.Focus();
                return;
            }
            else if (cboTenHocKy.SelectedValue == null)
            {
                MessageBox.Show("Bạn phải chọn học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenHocKy.Focus();
                return;
            }
            else if (cboTenMonHoc.SelectedValue == null)
            {
                MessageBox.Show("Bạn phải chọn môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenMonHoc.Focus();
                return;
            }
            else if (numHe10.Value == 0)
            {
                MessageBox.Show("Bạn phải nhập điểm hệ 10", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numHe10.Focus();
                return;
            }
            else if (cboXepLoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn xếp loại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboXepLoai.Focus();
                return;
            }
            else
            {
                string sql;
                string maHocKy = cboTenHocKy.SelectedValue.ToString();
                string maMonHoc = cboTenMonHoc.SelectedValue.ToString();

                if (string.IsNullOrEmpty(ma))
                {
                    // Kiểm tra điểm đã tồn tại chưa
                    sql = "SELECT Id FROM tblDiemHocTap WHERE MSSV = '" + cboMSSV.Text.Trim() + "' AND MaHocKy = '" + maHocKy + "' AND MaMonHoc = '" + maMonHoc + "'";
                    if (Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Điểm của sinh viên này đã tồn tại trong học kỳ và môn học này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sql = "INSERT INTO tblDiemHocTap(MSSV, MaHocKy, MaMonHoc, DiemHe10, DiemHe4, XepLoai) VALUES('" +
                          cboMSSV.Text.Trim() + "','" +
                          maHocKy + "','" +
                          maMonHoc + "'," +
                          numHe10.Value + "," +
                          numHe4.Value + ",N'" +
                          cboXepLoai.Text.Trim() + "')";
                }
                else
                {
                    sql = "UPDATE tblDiemHocTap SET MSSV = '" + cboMSSV.Text.Trim() +
                          "', MaHocKy = '" + maHocKy +
                          "', MaMonHoc = '" + maMonHoc +
                          "', DiemHe10 = " + numHe10.Value +
                          ", DiemHe4 = " + numHe4.Value +
                          ", XepLoai = N'" + cboXepLoai.Text.Trim() +
                          "' WHERE Id = " + ma;
                }
                Helper.Functions.RunSQL(sql);
                frmDiemHocTap_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDiemHocTap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một điểm để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa điểm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string id = dgvDiemHocTap.CurrentRow.Cells["Id"].Value.ToString();
                    string sql = "DELETE FROM tblDiemHocTap WHERE Id = " + id;
                    Helper.Functions.RunSQL(sql);
                    frmDiemHocTap_Load(sender, e);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            BatTat(false);
            frmDiemHocTap_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDataGridview()
        {
            string sql = "SELECT * FROM tblDiemHocTap";
            tblDiemHocTap = Helper.Functions.GetDataToTable(sql);
            dgvDiemHocTap.DataSource = tblDiemHocTap;
        }

        private void numHe10_ValueChanged(object sender, EventArgs e)
        {
            // Tự động tính điểm hệ 4 và xếp loại khi nhập điểm hệ 10
            float diemHe10 = (float)numHe10.Value;

            // Tính điểm hệ 4
            float diemHe4 = ConvertDiemHe10ToHe4(diemHe10);
            numHe4.Value = (decimal)diemHe4;

            // Tự động chọn xếp loại
            cboXepLoai.Text = GetXepLoai(diemHe10);
        }

        private float ConvertDiemHe10ToHe4(float diemHe10)
        {
            if (diemHe10 >= 8.5f) return 4.0f;
            if (diemHe10 >= 8.0f) return 3.5f;
            if (diemHe10 >= 7.0f) return 3.0f;
            if (diemHe10 >= 6.5f) return 2.5f;
            if (diemHe10 >= 5.5f) return 2.0f;
            if (diemHe10 >= 5.0f) return 1.5f;
            if (diemHe10 >= 4.0f) return 1.0f;
            return 0.0f;
        }

        private string GetXepLoai(float diemHe10)
        {
            if (diemHe10 >= 8.0f) return "Giỏi";
            if (diemHe10 >= 6.5f) return "Khá";
            if (diemHe10 >= 5.0f) return "Trung bình";
            return "Yếu";
        }
    }
}