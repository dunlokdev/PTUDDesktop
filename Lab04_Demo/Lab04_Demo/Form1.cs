using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab04_Demo
{
    public partial class frmMain : Form
    {
        DanhSachSinhVien dssv;
        public frmMain()
        {
            InitializeComponent();
        }

        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = this.mtbMSSV.Text;
            sv.HoTen = this.tbHoTen.Text;
            sv.Email = this.tbEmail.Text;
            sv.DiaChi = this.tbDiaChi.Text;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.GioiTinh = this.rdNam.Checked ? true : false;
            sv.Lop = this.cbLop.Text;
            sv.SDT = this.mtbSDT.Text;

            return sv;
        }

        private SinhVien GetSinhVienLV(ListViewItem lvItem)
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = lvItem.SubItems[0].Text;
            sv.HoTen = lvItem.SubItems[1].Text;
            sv.Email = lvItem.SubItems[2].Text;
            sv.DiaChi = lvItem.SubItems[3].Text;
            sv.NgaySinh = DateTime.Parse(lvItem.SubItems[4].Text);
            sv.GioiTinh = (lvItem.SubItems[5].Text) == "Nam" ? true : false;
            sv.Lop = lvItem.SubItems[6].Text;
            sv.SDT = lvItem.SubItems[7].Text;

            return sv;
        }

        private void ThietLapThongTin(SinhVien sv)
        {
            mtbMSSV.Text = sv.MaSo;
            tbHoTen.Text = sv.HoTen;
            tbEmail.Text = sv.Email;
            tbDiaChi.Text = sv.DiaChi;
            dtpNgaySinh.Value = sv.NgaySinh;
            rdNam.Checked = sv.GioiTinh ? true : false;
            cbLop.Text = sv.Lop;
            mtbSDT.Text = sv.SDT;
        }
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvItem = new ListViewItem(sv.MaSo);
            lvItem.SubItems.Add(sv.HoTen);
            lvItem.SubItems.Add(sv.Email);
            lvItem.SubItems.Add(sv.DiaChi);
            lvItem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            string gt = "Nu";
            if (sv.GioiTinh)
                gt = "Nam";
            lvItem.SubItems.Add(gt);
            lvItem.SubItems.Add(sv.Lop);
            lvItem.SubItems.Add(sv.SDT);
            lvSinhVien.Items.Add(lvItem);
        }

        private void LoadListView()
        {
            lvSinhVien.Items.Clear();
            foreach (SinhVien sv in dssv.DSSV)
            {
                ThemSV(sv);
            }
        }

        #region Sự kiện


        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            #region OpenFileDialog
            //OpenFileDialog fileDialog = pictureFileDialog1;

            //bool isAccept = fileDialog.ShowDialog() == DialogResult.OK;
            //if (isAccept)
            //{
            //    var arr = fileDialog.FileName.Split('\\');
            //    string fileName = arr[arr.Length - 1].Split('.')[0];
            //    string extension = arr[arr.Length - 1].Split('.')[1];

            //    // Make unique file name
            //    fileName += "_" + DateTime.UtcNow.ToBinary();

            //    string fullFileName = fileName + "." + extension;

            //    string projectPath = Utils.GetPathTo("data", "photos", fullFileName);

            //    // Copy file to /data in project folder
            //    File.Copy(fileDialog.FileName, projectPath);

            //    tbHinh.Text = projectPath;
            //    pbHinh.ImageLocation = projectPath;
            //}
            #endregion

            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Title = "Open Image File";
            fileOpen.Filter = "JPG Files (*.jpg)| *.jpg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pbHinh.Image = Image.FromFile(fileOpen.FileName);
            }
            fileOpen.Dispose();
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            mtbMSSV.Text = "";
            tbHoTen.Text = "";
            tbEmail.Text = "";
            tbDiaChi.Text = "";
            tbHinh.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            rdNam.Checked = true;
            cbLop.Text = this.cbLop.Items[0].ToString();
            mtbSDT.Text = "";
            pbHinh.ImageLocation = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            SinhVien.ShowExit();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //if (mtbMSSV.Text )
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dssv = new DanhSachSinhVien();
            dssv.DocTuFile();
            LoadListView();
        }
        #endregion
    }
}
