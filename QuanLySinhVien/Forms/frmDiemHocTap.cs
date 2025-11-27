using ClosedXML.Excel;
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
            dgvDiemHocTap.AutoGenerateColumns = false;
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
            string sql = @"SELECT d.*, sv.MSSV, sv.HoTen, hk.TenHocKy, mh.TenMonHoc
                           FROM tblDiemHocTap d
                           INNER JOIN tblSinhVien sv ON d.MSSV = sv.MSSV
                           INNER JOIN tblHocKy hk ON d.MaHocKy = hk.MaHocKy
                           INNER JOIN tblMonHoc mh ON d.MaMonHoc = mh.MaMonHoc";
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

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu Điểm Học Tập ra Excel";
            saveFileDialog.Filter = "Tập tin Excel |*.xls;*.xlsx";
            saveFileDialog.FileName = "DiemHocTap_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // SQL lấy dữ liệu Điểm Học Tập + Sinh viên + Lớp + Học kỳ + Môn học
                    string sql = @"SELECT d.MaSV, sv.HoTen, l.TenLop, d.MaHocKy, hk.TenHocKy,
                                  d.MaMonHoc, mh.TenMonHoc, d.Diem10, d.Diem4
                           FROM tblDiemHocTap d
                           INNER JOIN tblSinhVien sv ON d.MaSV = sv.MaSV
                           INNER JOIN tblLop l ON sv.MaLop = l.MaLop
                           INNER JOIN tblHocKy hk ON d.MaHocKy = hk.MaHocKy
                           INNER JOIN tblMonHoc mh ON d.MaMonHoc = mh.MaMonHoc";

                    DataTable table = Helper.Functions.GetDataToTable(sql);

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "DiemHocTap");
                        sheet.Columns().AdjustToContents(); // tự động điều chỉnh cột
                        wb.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Đã xuất dữ liệu Điểm Học Tập ra Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"SELECT d.*, sv.HoTen, hk.TenHocKy, mh.TenMonHoc
                   FROM tblDiemHocTap d
                   INNER JOIN tblSinhVien sv ON d.MSSV = sv.MSSV
                   INNER JOIN tblHocKy hk ON d.MaHocKy = hk.MaHocKy
                   INNER JOIN tblMonHoc mh ON d.MaMonHoc = mh.MaMonHoc
                   WHERE d.MSSV LIKE N'%" + key + "%' " +
                           "OR sv.HoTen LIKE N'%" + key + "%' " +
                           "OR hk.TenHocKy LIKE N'%" + key + "%' " +
                           "OR mh.TenMonHoc LIKE N'%" + key + "%' " +
                           "OR d.MaMonHoc LIKE N'%" + key + "%'" +
                           "OR d.XepLoai LIKE N'%" + key + "%'";

            tblDiemHocTap = Helper.Functions.GetDataToTable(sql);

            if (tblDiemHocTap.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblDiemHocTap.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvDiemHocTap.DataSource = tblDiemHocTap;

            txtTimKiem.Clear();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            frmDiemHocTap_Load(sender, e);
        }
    }
}