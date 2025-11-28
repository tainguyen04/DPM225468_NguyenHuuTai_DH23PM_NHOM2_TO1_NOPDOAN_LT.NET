using ClosedXML.Excel;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.Forms
{
    public partial class frmSinhVien: Form
    {
        DataTable tblSV;
        string ma;
        string imageName = "no-image.jpg";
        string imageFolder = Application.StartupPath.Replace("bin\\Debug", "Images");
        public frmSinhVien()
        {
            InitializeComponent();

        }
        public void BatTat(bool giaTri)
        {

            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
            txtMSSV.Enabled = giaTri;
            txtHoTen.Enabled = giaTri;
            rdbNam.Enabled = giaTri;
            rdbNu.Enabled = giaTri;
            txtSoDienThoai.Enabled = giaTri;
            txtDiaChi.Enabled = giaTri;
            dtpNgaySinh.Enabled = giaTri;
            cboTenLop.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
            //btnHuyBo.Enabled = giaTri;
            picHinhAnh.Enabled = giaTri;

        }
        public void LayDanhSachLop()
        {
            string sql = "SELECT * FROM tblLop";
            DataTable tblLop = Helper.Functions.GetDataToTable(sql);
            cboTenLop.DataSource = tblLop;
            cboTenLop.DisplayMember = "TenLop";
            cboTenLop.ValueMember = "MaLop";
        }
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            BatTat(false);
            dgvSinhVien.AutoGenerateColumns = false;
            LoadDataGridview();
            LayDanhSachLop();
            txtMSSV.DataBindings.Clear();
            txtMSSV.DataBindings.Add("Text", dgvSinhVien.DataSource, "MSSV", false, DataSourceUpdateMode.Never);

            txtHoTen.DataBindings.Clear();
            txtHoTen.DataBindings.Add("Text", dgvSinhVien.DataSource, "HoTen", false, DataSourceUpdateMode.Never);

            rdbNam.DataBindings.Clear();
            Binding gioiTinh = new Binding("Checked",dgvSinhVien.DataSource,"GioiTinh",true,DataSourceUpdateMode.Never);
            rdbNam.DataBindings.Add(gioiTinh);

            rdbNam.CheckedChanged += (s, args) =>
            {
                rdbNu.Checked = !rdbNam.Checked;
            };

            txtSoDienThoai.DataBindings.Clear();
            txtSoDienThoai.DataBindings.Add("Text", dgvSinhVien.DataSource, "SoDienThoai", false, DataSourceUpdateMode.Never);

            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", dgvSinhVien.DataSource, "DiaChi", false, DataSourceUpdateMode.Never);

            cboTenLop.DataBindings.Clear();
            cboTenLop.DataBindings.Add("SelectedValue", dgvSinhVien.DataSource, "MaLop", false, DataSourceUpdateMode.Never);

            picHinhAnh.DataBindings.Clear();
            Binding hinhAnh = new Binding("ImageLocation", dgvSinhVien.DataSource, "HinhAnh", false, DataSourceUpdateMode.Never);
            hinhAnh.Format += (s, args) =>
            {
                args.Value = Path.Combine(imageFolder, args.Value?.ToString() ?? "no-image.jpg");
            };
            picHinhAnh.DataBindings.Add(hinhAnh);
            dgvSinhVien.Refresh();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            BatTat(true);
            txtMSSV.Text = "";
            ma = "";
            txtHoTen.Text = "";
            rdbNam.Checked = false;
            rdbNu.Checked = false;
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cboTenLop.Text = "";
            picHinhAnh.Image = null;
            txtMSSV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            BatTat(true);
            txtMSSV.Enabled = false;
            if (dgvSinhVien.CurrentRow != null)
            {
                ma = dgvSinhVien.CurrentRow.Cells[0].Value.ToString();
                txtHoTen.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMSSV.Text))
            {
                MessageBox.Show("Vui lòng nhập MSSV.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMSSV.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen.Focus();
                return;
            }

            else if (!rdbNam.Checked && !rdbNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoDienThoai.Focus();
                return;
            }

            else if (cboTenLop.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTenLop.Focus();
                return;
            }

            else if (dtpNgaySinh.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return;
            }    
            else
            {
                string sql;
                // Tạo giá trị giới tính bit
                string gioiTinh = rdbNam.Checked ? "1" : "0";
                

                string maLop = cboTenLop.SelectedValue.ToString();
                int siSo = (int)Helper.Functions.GetFieldValues("SELECT SiSo FROM tblLop WHERE MaLop='" + maLop + "'");
                int soSV = (int)Helper.Functions.GetFieldValues("SELECT COUNT(*) FROM tblSinhVien WHERE MaLop='" + maLop + "'");
                if (soSV >= siSo)
                {
                    MessageBox.Show("Lớp này đã đầy. Không thể thêm sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboTenLop.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ma))
                {
                    sql = "SELECT MSSV FROM tblSinhVien WHERE MSSV = '" + txtMSSV.Text.Trim() + "'";
                    if(Helper.Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã số sinh viên đã tồn tại, bạn phải nhập mã số khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMSSV.Focus();
                        return;
                    }
                    sql = "INSERT INTO tblSinhVien(MSSV, HoTen, GioiTinh, SoDienThoai, DiaChi, NgaySinh, MaLop, HinhAnh) " +
                                 "VALUES('" + txtMSSV.Text.Trim() + "',N'" +
                                 txtHoTen.Text.Trim() + "'," +
                                 gioiTinh + ",'" +
                                 txtSoDienThoai.Text.Trim() + "',N'" +
                                 txtDiaChi.Text.Trim() + "','" +
                                 dtpNgaySinh.Value.ToString("yyyy-MM-dd") + "','" +
                                 cboTenLop.SelectedValue.ToString() + "','" +
                                 imageName + "')";
                }
                else
                {
                    string oldImageName = dgvSinhVien.CurrentRow.Cells["HinhAnh"].Value?.ToString() ?? "no-image.jpg";
                    string oldFilePath = Path.Combine(imageFolder, oldImageName);

                    if (File.Exists(oldFilePath) && oldImageName != "no-image.jpg")
                    {
                        try { File.Delete(oldFilePath); }
                        catch { }
                    }
                    sql = "UPDATE tblSinhVien SET " +
                        "HoTen = N'" + txtHoTen.Text.Trim() + "', " +
                        "GioiTinh = " + gioiTinh + ", " +
                        "SoDienThoai = '" + txtSoDienThoai.Text.Trim() + "', " +
                        "DiaChi = N'" + txtDiaChi.Text.Trim() + "', " +
                        "NgaySinh = '" + dtpNgaySinh.Value.ToString("yyyy-MM-dd") + "', " +
                        "MaLop = '" + cboTenLop.SelectedValue.ToString() + "', " +
                        "HinhAnh = '" + imageName + "' " +
                        "WHERE MSSV = '" + ma + "'";
                }
                Helper.Functions.RunSQL(sql);
                MessageBox.Show("Lưu thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                frmSinhVien_Load(sender,e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(MessageBox.Show("Xác nhận xóa sinh viên " + txtHoTen.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ma = dgvSinhVien.CurrentRow.Cells[0].Value.ToString();
                    string hinhAnh = dgvSinhVien.CurrentRow.Cells["HinhAnh"].Value?.ToString() ?? "";
                    if(!string.IsNullOrEmpty(hinhAnh) && hinhAnh != "no-image.jpg")
                    {
                        string filePath = Path.Combine(imageFolder, hinhAnh);
                        if (File.Exists(filePath))
                        {
                            try
                            {
                                File.Delete(filePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không xóa được file ảnh: " + ex.Message);
                            }
                        }
                    }
                    string sql = "DELETE FROM tblSinhVien WHERE MSSV = '" + ma + "'";
                    Helper.Functions.RunSQL(sql);
                    frmSinhVien_Load(sender, e);
                }
               
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void LoadDataGridview()
        {
            string sql = @"SELECT *
                   FROM tblSinhVien 
                   INNER JOIN tblLop ON tblSinhVien.MaLop = tblLop.MaLop";
            tblSV = Helper.Functions.GetDataToTable(sql);
            dgvSinhVien.DataSource = tblSV;
            dgvSinhVien.Refresh();
        }

        private void dgvSinhVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "HinhAnh")
            {
                string imagePath = Path.Combine(imageFolder, e.Value?.ToString() ?? "no-image.jpg");
                if (File.Exists(imagePath))
                {
                    try
                    {
                        using (var img = Image.FromFile(imagePath))
                        {
                            e.Value = new Bitmap(img, 24, 24); // resize copy mới
                        }
                    }
                    catch { e.Value = null; }
                }
                else
                {
                    e.Value = null; // hoặc ảnh mặc định
                }
            }
        }

        private void dgvSinhVien_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private void picHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn hình ảnh sinh viên";
            openFileDialog.Filter = "Tập tin hình ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                string ext = Path.GetExtension(openFileDialog.FileName);

                // Lưu tên file hình vào biến toàn cục 
                imageName = fileName.GenerateSlug() + ext;

                // Sao chép file hình vào thư mục Images 
                string fileSavePath = Path.Combine(imageFolder, imageName);
                if (picHinhAnh.Image != null)
                {
                    picHinhAnh.Image.Dispose();
                    picHinhAnh.Image = null;
                }

                

                File.Copy(openFileDialog.FileName, fileSavePath, true);

                // Hiện hình ảnh đã chọn lên PictureBox 
                using (var img = Image.FromFile(fileSavePath))
                {
                    picHinhAnh.Image = new Bitmap(img); // copy ảnh vào PictureBox
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

            string sql = @"SELECT *
                   FROM tblSinhVien sv
                   INNER JOIN tblLop l ON sv.MaLop = l.MaLop
                   WHERE sv.MSSV LIKE N'%" + key + "%' " +
                           "OR sv.HoTen LIKE N'%" + key + "%' " +
                           "OR sv.MaLop LIKE N'%" + key + "%' " +
                           "OR l.TenLop LIKE N'%" + key + "%'";

            tblSV = Helper.Functions.GetDataToTable(sql);

            if (tblSV.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblSV.Rows.Count + " bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvSinhVien.DataSource = tblSV;
            dgvSinhVien.Refresh();

            txtTimKiem.Clear();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel |*.xls;*.xlsx";
            saveFileDialog.FileName = "ThongTinSinhVien_" + DateTime.Now.ToString("yyyy_MM_dd") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // SQL lấy dữ liệu sinh viên + lớp
                    string sql = @"SELECT sv.MSSV, sv.HoTen, sv.GioiTinh, sv.SoDienThoai, sv.DiaChi, sv.NgaySinh,
                                  sv.MaLop, l.TenLop, sv.HinhAnh
                           FROM tblSinhVien sv
                           INNER JOIN tblLop l ON sv.MaLop = l.MaLop";

                    DataTable table = Helper.Functions.GetDataToTable(sql);

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "ThongTinSinhVien");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Đã xuất dữ liệu ra tập tin Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmSinhVien_Load(sender, e);
        }

       
    }
}
