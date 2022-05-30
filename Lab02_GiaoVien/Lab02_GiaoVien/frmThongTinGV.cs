using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_GiaoVien
{
    public partial class frmGiaoVien : Form
    {
        public frmGiaoVien()
        {
            InitializeComponent();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            string lienHe = "https://github.com/myloc442";
            linklbLienHe.Links.Add(0, lienHe.Length, lienHe);
            cboMaSo.SelectedItem = this.cboMaSo.Items[0];
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            int i = lbDanhSachMH.SelectedItems.Count - 1;

            while (i >= 0)
            {
                lbMonHocDay.Items.Add(lbDanhSachMH.SelectedItems[i]);
                lbDanhSachMH.Items.Remove(lbDanhSachMH.SelectedItems[i]);
                i--;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int i = lbMonHocDay.SelectedItems.Count - 1;

            while (i >= 0)
            {
                lbDanhSachMH.Items.Add(lbMonHocDay.SelectedItems[i]);
                lbMonHocDay.Items.Remove(lbMonHocDay.SelectedItems[i]);
                i--;
            }
        }
    }
}
