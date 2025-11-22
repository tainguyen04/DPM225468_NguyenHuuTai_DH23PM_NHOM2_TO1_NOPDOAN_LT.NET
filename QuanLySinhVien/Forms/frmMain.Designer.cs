namespace QuanLySinhVien.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSinhVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHocKy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonHoc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHocTap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDiemHocTap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDiemRenLuyen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBangDiem = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachSinhVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBangDiemTongHop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuThongKeTheoLop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKeTheoDHT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKeTheoDRL = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHuongDan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGioiThieu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuQuanLy,
            this.mnuHocTap,
            this.báoCáoToolStripMenuItem,
            this.trợGiúpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1215, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDangNhap,
            this.mnuDangXuat,
            this.toolStripSeparator1,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(103, 29);
            this.mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuDangNhap
            // 
            this.mnuDangNhap.Image = global::QuanLySinhVien.Properties.Resources.dang_nhap;
            this.mnuDangNhap.Name = "mnuDangNhap";
            this.mnuDangNhap.Size = new System.Drawing.Size(270, 34);
            this.mnuDangNhap.Text = "&Đăng nhập";
            this.mnuDangNhap.Click += new System.EventHandler(this.mnuDangNhap_Click);
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Image = global::QuanLySinhVien.Properties.Resources.dang_xuat;
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(270, 34);
            this.mnuDangXuat.Text = "&Đăng Xuất";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Image = global::QuanLySinhVien.Properties.Resources.thoat;
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuThoat.Size = new System.Drawing.Size(270, 34);
            this.mnuThoat.Text = "&Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // mnuQuanLy
            // 
            this.mnuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSinhVien,
            this.mnuLop,
            this.mnuKhoa,
            this.toolStripSeparator2,
            this.mnuHocKy,
            this.mnuMonHoc,
            this.toolStripSeparator3,
            this.mnuTaiKhoan});
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Size = new System.Drawing.Size(89, 29);
            this.mnuQuanLy.Text = "&Quản lý";
            // 
            // mnuSinhVien
            // 
            this.mnuSinhVien.Image = global::QuanLySinhVien.Properties.Resources.user1;
            this.mnuSinhVien.Name = "mnuSinhVien";
            this.mnuSinhVien.Size = new System.Drawing.Size(270, 34);
            this.mnuSinhVien.Text = "&Sinh viên";
            this.mnuSinhVien.Click += new System.EventHandler(this.mnuSinhVien_Click);
            // 
            // mnuLop
            // 
            this.mnuLop.Name = "mnuLop";
            this.mnuLop.Size = new System.Drawing.Size(270, 34);
            this.mnuLop.Text = "&Lớp";
            this.mnuLop.Click += new System.EventHandler(this.mnuLop_Click);
            // 
            // mnuKhoa
            // 
            this.mnuKhoa.Name = "mnuKhoa";
            this.mnuKhoa.Size = new System.Drawing.Size(270, 34);
            this.mnuKhoa.Text = "&Khoa";
            this.mnuKhoa.Click += new System.EventHandler(this.mnuKhoa_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(267, 6);
            // 
            // mnuHocKy
            // 
            this.mnuHocKy.Name = "mnuHocKy";
            this.mnuHocKy.Size = new System.Drawing.Size(270, 34);
            this.mnuHocKy.Text = "&Học kỳ";
            this.mnuHocKy.Click += new System.EventHandler(this.mnuHocKy_Click);
            // 
            // mnuMonHoc
            // 
            this.mnuMonHoc.Name = "mnuMonHoc";
            this.mnuMonHoc.Size = new System.Drawing.Size(270, 34);
            this.mnuMonHoc.Text = "&Môn học";
            this.mnuMonHoc.Click += new System.EventHandler(this.mnuMonHoc_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(267, 6);
            // 
            // mnuTaiKhoan
            // 
            this.mnuTaiKhoan.Name = "mnuTaiKhoan";
            this.mnuTaiKhoan.Size = new System.Drawing.Size(270, 34);
            this.mnuTaiKhoan.Text = "&Tài khoản";
            this.mnuTaiKhoan.Click += new System.EventHandler(this.mnuTaiKhoan_Click);
            // 
            // mnuHocTap
            // 
            this.mnuHocTap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDiemHocTap,
            this.mnuDiemRenLuyen,
            this.toolStripSeparator4,
            this.mnuBangDiem});
            this.mnuHocTap.Name = "mnuHocTap";
            this.mnuHocTap.Size = new System.Drawing.Size(91, 29);
            this.mnuHocTap.Text = "&Học tập";
            // 
            // mnuDiemHocTap
            // 
            this.mnuDiemHocTap.Name = "mnuDiemHocTap";
            this.mnuDiemHocTap.Size = new System.Drawing.Size(270, 34);
            this.mnuDiemHocTap.Text = "&Điểm học tập";
            this.mnuDiemHocTap.Click += new System.EventHandler(this.mnuDiemHocTap_Click);
            // 
            // mnuDiemRenLuyen
            // 
            this.mnuDiemRenLuyen.Name = "mnuDiemRenLuyen";
            this.mnuDiemRenLuyen.Size = new System.Drawing.Size(270, 34);
            this.mnuDiemRenLuyen.Text = "&Điểm rèn luyện";
            this.mnuDiemRenLuyen.Click += new System.EventHandler(this.mnuDiemRenLuyen_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(267, 6);
            // 
            // mnuBangDiem
            // 
            this.mnuBangDiem.Name = "mnuBangDiem";
            this.mnuBangDiem.Size = new System.Drawing.Size(270, 34);
            this.mnuBangDiem.Text = "&Bảng điểm";
            this.mnuBangDiem.Click += new System.EventHandler(this.mnuBangDiem_Click);
            // 
            // báoCáoToolStripMenuItem
            // 
            this.báoCáoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDanhSachSinhVien,
            this.mnuBangDiemTongHop,
            this.mnuThongKe});
            this.báoCáoToolStripMenuItem.Name = "báoCáoToolStripMenuItem";
            this.báoCáoToolStripMenuItem.Size = new System.Drawing.Size(91, 29);
            this.báoCáoToolStripMenuItem.Text = "&Báo cáo";
            // 
            // mnuDanhSachSinhVien
            // 
            this.mnuDanhSachSinhVien.Name = "mnuDanhSachSinhVien";
            this.mnuDanhSachSinhVien.Size = new System.Drawing.Size(279, 34);
            this.mnuDanhSachSinhVien.Text = "&Danh sách sinh viên";
            this.mnuDanhSachSinhVien.Click += new System.EventHandler(this.mnuDanhSachSinhVien_Click);
            // 
            // mnuBangDiemTongHop
            // 
            this.mnuBangDiemTongHop.Name = "mnuBangDiemTongHop";
            this.mnuBangDiemTongHop.Size = new System.Drawing.Size(279, 34);
            this.mnuBangDiemTongHop.Text = "&Bảng điểm tổng hợp";
            this.mnuBangDiemTongHop.Click += new System.EventHandler(this.mnuBangDiemTongHop_Click);
            // 
            // mnuThongKe
            // 
            this.mnuThongKe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuThongKeTheoLop,
            this.mnuThongKeTheoDHT,
            this.mnuThongKeTheoDRL});
            this.mnuThongKe.Name = "mnuThongKe";
            this.mnuThongKe.Size = new System.Drawing.Size(279, 34);
            this.mnuThongKe.Text = "&Thống kê";
            // 
            // MnuThongKeTheoLop
            // 
            this.MnuThongKeTheoLop.Name = "MnuThongKeTheoLop";
            this.MnuThongKeTheoLop.Size = new System.Drawing.Size(351, 34);
            this.MnuThongKeTheoLop.Text = "&Thống kê theo lớp";
            this.MnuThongKeTheoLop.Click += new System.EventHandler(this.MnuThongKeTheoLop_Click);
            // 
            // mnuThongKeTheoDHT
            // 
            this.mnuThongKeTheoDHT.Name = "mnuThongKeTheoDHT";
            this.mnuThongKeTheoDHT.Size = new System.Drawing.Size(351, 34);
            this.mnuThongKeTheoDHT.Text = "&Thống kê theo học lực";
            this.mnuThongKeTheoDHT.Click += new System.EventHandler(this.mnuThongKeTheoDHT_Click);
            // 
            // mnuThongKeTheoDRL
            // 
            this.mnuThongKeTheoDRL.Name = "mnuThongKeTheoDRL";
            this.mnuThongKeTheoDRL.Size = new System.Drawing.Size(351, 34);
            this.mnuThongKeTheoDRL.Text = "&Thống kê theo điểm rèn luyện";
            this.mnuThongKeTheoDRL.Click += new System.EventHandler(this.mnuThongKeTheoDRL_Click);
            // 
            // trợGiúpToolStripMenuItem
            // 
            this.trợGiúpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHuongDan,
            this.mnuGioiThieu});
            this.trợGiúpToolStripMenuItem.Name = "trợGiúpToolStripMenuItem";
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(93, 29);
            this.trợGiúpToolStripMenuItem.Text = "&Trợ giúp";
            // 
            // mnuHuongDan
            // 
            this.mnuHuongDan.Name = "mnuHuongDan";
            this.mnuHuongDan.Size = new System.Drawing.Size(205, 34);
            this.mnuHuongDan.Text = "&Hướng dẫn";
            // 
            // mnuGioiThieu
            // 
            this.mnuGioiThieu.Name = "mnuGioiThieu";
            this.mnuGioiThieu.Size = new System.Drawing.Size(205, 34);
            this.mnuGioiThieu.Text = "&Giới thiệu";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 702);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Quản lý sinh viên";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDangNhap;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem mnuSinhVien;
        private System.Windows.Forms.ToolStripMenuItem mnuLop;
        private System.Windows.Forms.ToolStripMenuItem mnuKhoa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHocKy;
        private System.Windows.Forms.ToolStripMenuItem mnuMonHoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem mnuHocTap;
        private System.Windows.Forms.ToolStripMenuItem mnuDiemHocTap;
        private System.Windows.Forms.ToolStripMenuItem mnuDiemRenLuyen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuBangDiem;
        private System.Windows.Forms.ToolStripMenuItem báoCáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachSinhVien;
        private System.Windows.Forms.ToolStripMenuItem mnuBangDiemTongHop;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKe;
        private System.Windows.Forms.ToolStripMenuItem MnuThongKeTheoLop;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKeTheoDHT;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKeTheoDRL;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuHuongDan;
        private System.Windows.Forms.ToolStripMenuItem mnuGioiThieu;
    }
}