using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04_Demo
{
    public class SinhVien
    {
        // Fields:
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Hinh { get; set; }

        // Constructors
        public SinhVien() { }
        public SinhVien(string maSo, string hoTen, bool gioiTinh, DateTime ngaySinh, string lop, string sDT, string email, string diaChi, string hinh)
        {
            MaSo = maSo;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            Lop = lop;
            SDT = sDT;
            Email = email;
            DiaChi = diaChi;
            Hinh = hinh;
        }

        // Methods

        public static void ShowExit()
        {
            var dr = MessageBox.Show("Lộc thích Y.. phải không, chọn có để thoát", "Lộc thích Y", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)//Application.Exit();
            {
                MessageBox.Show("Lộc thừa nhận thích Y.. rồi haha", "Lộc thích Y");
            }
        }
    }
}
