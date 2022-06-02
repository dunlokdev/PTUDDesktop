using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_DL
{
    public partial class frmSinhVien : Form
    {
        QuanLySinhVien qlsv;

        public frmSinhVien()
        {
            InitializeComponent();
        }

        #region Phương thức hỗ trợ Form
        // Lấy thông tin từ Control thông tin sinh viên
        private SinhVien LaySinhVienControl()
        {
            SinhVien sv = new SinhVien();
            List<string> listChuyenNganh = new List<string>();

            sv.MaSo = this.mtxtMaSo.Text;
            sv.HoTen = this.txtHoTen.Text;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.Lop = this.clbChuyenNganh.Text;
            sv.Hinh = this.txtHinh.Text;
            sv.GioiTinh = rdNu.Checked ? true : false;
            for (int i = 0; i < clbChuyenNganh.Items.Count; i++)
            {
                if (clbChuyenNganh.GetItemChecked(i))
                {
                    listChuyenNganh.Add(clbChuyenNganh.Items[i].ToString());
                }
            }
            sv.ListChuyenNganh = listChuyenNganh;
            return sv;
        }

        // Lấy thông sinh viên từ dòng item của ListView
        private SinhVien LaySinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = lvitem.SubItems[0].Text;
            sv.HoTen = lvitem.SubItems[1].Text;
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[2].Text);
            sv.DiaChi = lvitem.SubItems[3].Text;
            sv.Lop = lvitem.SubItems[4].Text;
            sv.GioiTinh = false;
            if (lvitem.SubItems[5].Text == "Nam") sv.GioiTinh = true;
            List<string> cn = new List<string>();
            string[] ChuyenNganhLV = lvitem.SubItems[6].Text.Split(',');
            foreach (string chuyenNganh in ChuyenNganhLV)
            {
                cn.Add(chuyenNganh.Trim());
            }
            sv.ListChuyenNganh = cn;
            sv.Hinh = lvitem.SubItems[7].Text;

            return sv;
        }

        // Thiết lập các thông tin controls sinh viên
        private void ThietLapThongTin(SinhVien sv)
        {
            mtxtMaSo.Text = sv.MaSo;
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh;
            txtDiaChi.Text = sv.DiaChi;
            cboLop.Text = sv.Lop;
            txtHinh.Text = sv.Hinh;
            pbHinh.ImageLocation = sv.Hinh;
            if (sv.GioiTinh)
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
            for (int i = 0; i < clbChuyenNganh.Items.Count; i++)
            {
                clbChuyenNganh.SetItemChecked(i, false);
            }
            foreach (string chuyenNganh in sv.ListChuyenNganh)
            {
                for (int i = 0; i < clbChuyenNganh.Items.Count; i++)
                {
                    if (chuyenNganh.CompareTo(clbChuyenNganh.Items[i]) == 0)
                        clbChuyenNganh.SetItemChecked(i, true);
                }
            }

        }
        private void ThemSinhVienListView(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MaSo);
            lvitem.SubItems.Add(sv.HoTen);
            lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.Lop);

            string gt = "Nữ";
            if (sv.GioiTinh)
                gt = "Nam";
            lvitem.SubItems.Add(gt);

            string cn = "";
            foreach (string chuyenNganh in sv.ListChuyenNganh)
            {
                cn += chuyenNganh + ",";
            }
            cn = cn.Substring(0, cn.Length - 1);
            lvitem.SubItems.Add(cn);
            lvitem.SubItems.Add(sv.Hinh);

            lvSinhVien.Items.Add(lvitem);
        }

        private void LoadListView()
        {
            lvSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.DanhSachSinhVien)
            {
                ThemSinhVienListView(sv);
            }
        }

        #endregion

        #region Sự kiện

        // Sự kiện load khi Form khởi động
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();
        }

        // Khi chọn vào dòng sinh viên trên ListView, thực hiện gán thông tin sinh viên đó lên Controls
        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvitem = lvSinhVien.SelectedItems[0];
                SinhVien sv = LaySinhVienLV(lvitem);
                ThietLapThongTin(sv);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SinhVien sv = LaySinhVienControl();

            SinhVien result = qlsv.Tim(sv.MaSo, delegate (object obj1, object obj2)
            {
                return (obj2 as SinhVien).MaSo.CompareTo(obj1.ToString());
            });

            if (result != null)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi thêm dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                qlsv.ThemSinhVien(sv);
                LoadListView();
            }

        }
        #endregion
    }
}
