using ClosedXML.Excel;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLySinhVien.Forms
{
    public partial class frmDiemRenLuyen : Form
    {
        DataTable tblDiemRenLuyen;
        string ma;
        public frmDiemRenLuyen()
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
            numDiemRenLuyen.Enabled = giaTri;
            cboXepLoai.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
            //btnHuy.Enabled = giaTri;
        }

        private void LayDanhSachSinhVien()
        {
            string sql = "SELECT * FROM tblSinhVien";
            DataTable tblSinhVien = Helper.Functions.GetDataToTable(sql);
            cboMSSV.DataSource = tblSinhVien;
            cboMSSV.DisplayMember = "MSSV";
            cboMSSV.ValueMember = "MSSV";
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
            cboXepLoai.Items.Add("Xuất sắc");
            cboXepLoai.Items.Add("Tốt");
            cboXepLoai.Items.Add("Khá");
            cboXepLoai.Items.Add("Trung bình");
            cboXepLoai.Items.Add("Yếu");
            cboXepLoai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmDiemRenLuyen_Load(object sender, EventArgs e)
        {
            dgvDiemRenLuyen.AutoGenerateColumns = false;
            BatTat(false);
            LayDanhSachSinhVien();
            LayDanhSachHocKy();
            LoadXepLoai();
            LoadDataGridview();

            // Data binding
            cboMSSV.DataBindings.Clear();
            cboMSSV.DataBindings.Add("Text", dgvDiemRenLuyen.DataSource, "MSSV", false, DataSourceUpdateMode.Never);
            cboTenHocKy.DataBindings.Clear();
            cboTenHocKy.DataBindings.Add("SelectedValue", dgvDiemRenLuyen.DataSource, "MaHocKy", false, DataSourceUpdateMode.Never);
            numDiemRenLuyen.DataBindings.Clear();
            numDiemRenLuyen.DataBindings.Add("Value", dgvDiemRenLuyen.DataSource, "DiemRenLuyen", false, DataSourceUpdateMode.Never);
            cboXepLoai.DataBindings.Clear();
            cboXepLoai.DataBindings.Add("Text", dgvDiemRenLuyen.DataSource, "XepLoai", false, DataSourceUpdateMode.Never);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            BatTat(true);
            ma = "";
            cboMSSV.Text = "";
            cboTenHocKy.Text = "";
            numDiemRenLuyen.Value = 0;
            cboXepLoai.Text = "";
            cboMSSV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            if (dgvDiemRenLuyen.CurrentCell == null)
            {
                MessageBox.Show("Vui lòng chọn một Điểm rèn luyện để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                ma = dgvDiemRenLuyen.CurrentRow.Cells["Id"].Value.ToString();
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
            else if (numDiemRenLuyen.Value == 0)
            {
                MessageBox.Show("Bạn phải nhập điểm rèn luyện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDiemRenLuyen.Focus();
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

                if (string.IsNullOrEmpty(ma))
                {
                    // Kiểm tra điểm rèn luyện đã tồn tại chưa
                    sql = "SELECT Id FROM tblDiemRenLuyen WHERE MSSV = '" + cboMSSV.Text.Trim() + "' AND MaHocKy = '" + maHocKy + "'";
                    if (Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Điểm rèn luyện của sinh viên này đã tồn tại trong học kỳ này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sql = "INSERT INTO tblDiemRenLuyen(MSSV, MaHocKy, DiemRenLuyen, XepLoai) VALUES('" +
                          cboMSSV.Text.Trim() + "','" +
                          maHocKy + "'," +
                          numDiemRenLuyen.Value + ",N'" +
                          cboXepLoai.Text.Trim() + "')";
                }
                else
                {
                    sql = "UPDATE tblDiemRenLuyen SET MSSV = '" + cboMSSV.Text.Trim() +
                          "', MaHocKy = '" + maHocKy +
                          "', DiemRenLuyen = " + numDiemRenLuyen.Value +
                          ", XepLoai = N'" + cboXepLoai.Text.Trim() +
                          "' WHERE Id = " + ma;
                }
                Helper.Functions.RunSQL(sql);
                frmDiemRenLuyen_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDiemRenLuyen.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một điểm rèn luyện để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa điểm rèn luyện này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string id = dgvDiemRenLuyen.CurrentRow.Cells["Id"].Value.ToString();
                    string sql = "DELETE FROM tblDiemRenLuyen WHERE Id = " + id;
                    Helper.Functions.RunSQL(sql);
                    frmDiemRenLuyen_Load(sender, e);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            BatTat(false);
            frmDiemRenLuyen_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDataGridview()
        {
            string sql = @"SELECT drl.Id, drl.MSSV, sv.HoTen, drl.MaHocKy, hk.TenHocKy, 
                          drl.DiemRenLuyen, drl.XepLoai 
                   FROM tblDiemRenLuyen drl
                   INNER JOIN tblSinhVien sv ON drl.MSSV = sv.MSSV
                   INNER JOIN tblHocKy hk ON drl.MaHocKy = hk.MaHocKy";
            tblDiemRenLuyen = Helper.Functions.GetDataToTable(sql);
            dgvDiemRenLuyen.DataSource = tblDiemRenLuyen;
        }

        private void numDiemRenLuyen_ValueChanged(object sender, EventArgs e)
        {
            // Tự động chọn xếp loại khi nhập điểm rèn luyện
            int diem = (int)numDiemRenLuyen.Value;
            cboXepLoai.Text = GetXepLoaiRenLuyen(diem);
        }

        private string GetXepLoaiRenLuyen(int diem)
        {
            if (diem >= 90) return "Xuất sắc";
            if (diem >= 80) return "Tốt";
            if (diem >= 70) return "Khá";
            if (diem >= 50) return "Trung bình";
            return "Yếu";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = @"SELECT drl.Id, drl.MSSV, sv.HoTen, drl.MaHocKy, hk.TenHocKy, 
                          drl.DiemRenLuyen, drl.XepLoai
                   FROM tblDiemRenLuyen drl
                   INNER JOIN tblSinhVien sv ON drl.MSSV = sv.MSSV
                   INNER JOIN tblHocKy hk ON drl.MaHocKy = hk.MaHocKy
                   WHERE drl.MSSV LIKE N'%" + key + "%' " +
                           "OR sv.HoTen LIKE N'%" + key + "%' " +
                           "OR hk.TenHocKy LIKE N'%" + key + "%' " +
                           "OR drl.XepLoai LIKE N'%" + key + "%'";

            tblDiemRenLuyen = Helper.Functions.GetDataToTable(sql);

            if (tblDiemRenLuyen.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblDiemRenLuyen.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvDiemRenLuyen.DataSource = tblDiemRenLuyen;

            txtTimKiem.Clear();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu Điểm Rèn Luyện ra Excel";
            saveFileDialog.Filter = "Tập tin Excel |*.xls;*.xlsx";
            saveFileDialog.FileName = "DiemRenLuyen_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // SQL lấy dữ liệu Điểm Rèn Luyện + Sinh viên + Học kỳ
                    string sql = @"SELECT drl.Id, drl.MSSV, sv.HoTen, drl.MaHocKy, hk.TenHocKy,
                                  drl.DiemRenLuyen, drl.XepLoai
                           FROM tblDiemRenLuyen drl
                           INNER JOIN tblSinhVien sv ON drl.MSSV = sv.MSSV
                           INNER JOIN tblHocKy hk ON drl.MaHocKy = hk.MaHocKy";

                    DataTable table = Helper.Functions.GetDataToTable(sql);

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "DiemRenLuyen");
                        sheet.Columns().AdjustToContents(); // tự động điều chỉnh cột
                        wb.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Đã xuất dữ liệu Điểm Rèn Luyện ra Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            frmDiemRenLuyen_Load(sender, e);
        }

        
    }
}