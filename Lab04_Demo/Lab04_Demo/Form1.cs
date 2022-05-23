using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04_Demo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = pictureFileDialog1;

            bool isAccept = fileDialog.ShowDialog() == DialogResult.OK;
            if (isAccept)
            {
                var arr = fileDialog.FileName.Split('\\');
                string fileName = arr[arr.Length - 1].Split('.')[0];
                string extension = arr[arr.Length - 1].Split('.')[1];

                // Make unique file name
                fileName += "_" + DateTime.UtcNow.ToBinary();

                string fullFileName = fileName + "." + extension;

                string projectPath = Utils.GetPathTo("data", "photos", fullFileName);

                // Copy file to /data in project folder
                File.Copy(fileDialog.FileName, projectPath);

                tbHinh.Text = projectPath;
                pbHinh.ImageLocation = projectPath;
            }
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            this.mtbMSSV.Text = "";
            this.tbHoTen.Text = "";
            this.tbEmail.Text = "";
            this.tbDiaChi.Text = "";
            this.tbHinh.Text = "";
            this.dtpNgaySinh.Value = DateTime.Now;
            this.rdNam.Checked = true;
            this.cbLop.Text = this.cbLop.Items[0].ToString();
            this.mtbSDT.Text = "";
            this.pbHinh.ImageLocation = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
