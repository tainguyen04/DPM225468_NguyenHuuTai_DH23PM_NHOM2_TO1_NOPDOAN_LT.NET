using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySinhVien.Helper;
using static System.Collections.Specialized.BitVector32;
namespace QuanLySinhVien.Forms
{
    public partial class frmMain: Form
    {
        private string _quyen;
        public frmMain()
        {
            InitializeComponent();
            statusStrip1.Items.Add(toolStripStatusLabel1);
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            ChuaPhanQuyen();
            Functions.Connect();
            frmDangNhap frmDangNhap = new frmDangNhap();
            if (frmDangNhap.ShowDialog() == DialogResult.OK)
            {
                _quyen = frmDangNhap.Quyen;
                PhanQuyen();
            }
        }
        public void ChuaPhanQuyen()
        {
            mnuQuanLy.Enabled = false;
            mnuHocTap.Enabled = false;
            
            mnuDangXuat.Enabled = false;

            mnuDangNhap.Enabled = true;
            
      
        }
        public void QuyenNhanVien()
        {
            mnuDangNhap.Enabled = false;
            mnuTaiKhoan.Enabled = false;
            mnuLop.Enabled = false;
            mnuKhoa.Enabled = false;
            mnuHocKy.Enabled = false;

            mnuDangXuat.Enabled = true;
            mnuQuanLy.Enabled = true;
            mnuHocTap.Enabled = true;
            
        }
        public void QuyenAdmin()
        {
            mnuDangNhap.Enabled = false;

            mnuDangXuat.Enabled = true;
            mnuQuanLy.Enabled = true;
            mnuHocTap.Enabled = true;

        }
        public void PhanQuyen()
        {
           
            if (_quyen == "Admin")
                QuyenAdmin();
            else if(_quyen == "NhanVien")
                QuyenNhanVien();

            toolStripStatusLabel1.Text = "Vai trò: "  + _quyen;
        }
        frmDangNhap DangNhap = null;
        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            if(DangNhap == null || DangNhap.IsDisposed)
            {
                DangNhap = new frmDangNhap();
                if (DangNhap.ShowDialog() == DialogResult.OK)
                {
                    _quyen = DangNhap.Quyen;
                    PhanQuyen();
                }
                DangNhap = null;
            }
            else
            {
                DangNhap.Activate();
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect();
            Application.Exit();
        }
        frmSinhVien frmSinhVien = null;

        private void mnuSinhVien_Click(object sender, EventArgs e)
        {
            if (frmSinhVien == null || frmSinhVien.IsDisposed)
            {
                frmSinhVien = new frmSinhVien();
                frmSinhVien.MdiParent = this;
                frmSinhVien.Show();
            }
            else
            {
                frmSinhVien.Activate();
            }

        }
        frmLop frmLop = null;

        private void mnuLop_Click(object sender, EventArgs e)
        {
            if (frmLop == null || frmLop.IsDisposed)
            {
                frmLop = new frmLop();
                frmLop.MdiParent = this;
                frmLop.Show();
            }
            else
            {
                frmLop.Activate();
            }
        }
        frmKhoa frmKhoa = null;

        private void mnuKhoa_Click(object sender, EventArgs e)
        {
            if (frmKhoa == null || frmKhoa.IsDisposed)
            {
                frmKhoa = new frmKhoa();
                frmKhoa.MdiParent = this;
                frmKhoa.Show();
            }
            else
            {
                frmKhoa.Activate();
            }
        }
        frmHocKy frmHocKy = null;

        private void mnuHocKy_Click(object sender, EventArgs e)
        {
            if (frmHocKy == null || frmHocKy.IsDisposed)
            {
                frmHocKy = new frmHocKy();
                frmHocKy.MdiParent = this;
                frmHocKy.Show();
            }
            else
            {
                frmHocKy.Activate();
            }
        }
        frmMonHoc frmMonHoc = null;
        private void mnuMonHoc_Click(object sender, EventArgs e)
        {
            if (frmMonHoc == null || frmMonHoc.IsDisposed)
            {
                frmMonHoc = new frmMonHoc();
                frmMonHoc.MdiParent = this;
                frmMonHoc.Show();
            }
            else
            {
                frmMonHoc.Activate();
            }   
        }
        frmTaiKhoan frmTaiKhoan = null;
        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            if (frmTaiKhoan == null || frmTaiKhoan.IsDisposed)
            {
                frmTaiKhoan = new frmTaiKhoan();
                frmTaiKhoan.MdiParent = this;
                frmTaiKhoan.Show();
            }
            else
            {
                frmTaiKhoan.Activate();
            }
        }
        frmDiemHocTap frmDiemHocTap = null;
        private void mnuDiemHocTap_Click(object sender, EventArgs e)
        {
            if(frmDiemHocTap == null || frmDiemHocTap.IsDisposed)
            {
                frmDiemHocTap= new frmDiemHocTap();
                frmDiemHocTap.MdiParent = this;
                frmDiemHocTap.Show();
            }
            else
            {
                frmDiemHocTap.Activate();
            }
        }
        frmDiemRenLuyen frmDiemRenLuyen = null;
        private void mnuDiemRenLuyen_Click(object sender, EventArgs e)
        {
            if (frmDiemRenLuyen == null || frmDiemRenLuyen.IsDisposed) 
            {
                frmDiemRenLuyen= new frmDiemRenLuyen();
                frmDiemRenLuyen.MdiParent= this;
                frmDiemRenLuyen.Show();
            }
            else
            {
                frmDiemRenLuyen.Activate();
            }
        }

      

        
        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            _quyen = "";
            ChuaPhanQuyen();
        }

        private void mnuHuongDan_Click(object sender, EventArgs e)
        {
            string githubUrl = "https://github.com/tainguyen04/DPM225468_NguyenHuuTai_DH23PM_NHOM2_TO1_NOPDOAN_LT.NET/blob/main/README.md";

            
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = githubUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở hướng dẫn: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
