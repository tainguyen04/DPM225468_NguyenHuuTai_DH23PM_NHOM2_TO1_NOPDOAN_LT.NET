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
namespace QuanLySinhVien.Forms
{
    public partial class frmMain: Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            frmDangNhap frmDangNhap = new frmDangNhap();
            if (frmDangNhap.ShowDialog() == DialogResult.OK)
            {
                // Đăng nhập thành công
            }
        }
        frmDangNhap frmDangNhap = null;
        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            if(frmDangNhap == null || frmDangNhap.IsDisposed)
            {
                frmDangNhap = new frmDangNhap();
                if (frmDangNhap.ShowDialog() == DialogResult.OK)
                {
                    // Đăng nhập thành công
                }
            }
            else
            {
                frmDangNhap.Activate();
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

        private void mnuBangDiem_Click(object sender, EventArgs e)
        {

        }

        private void mnuDanhSachSinhVien_Click(object sender, EventArgs e)
        {

        }

        private void mnuBangDiemTongHop_Click(object sender, EventArgs e)
        {

        }

        private void MnuThongKeTheoLop_Click(object sender, EventArgs e)
        {

        }

        private void mnuThongKeTheoDHT_Click(object sender, EventArgs e)
        {

        }

        private void mnuThongKeTheoDRL_Click(object sender, EventArgs e)
        {

        }
    }
}
