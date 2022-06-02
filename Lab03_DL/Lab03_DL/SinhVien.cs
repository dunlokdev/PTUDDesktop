using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_DL
{
    public class SinhVien
    {
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Lop { get; set; }
        public string Hinh { get; set; }
        public bool GioiTinh { get; set; }
        public List<string> ListChuyenNganh { get; set; }

        public SinhVien()
        {
            ListChuyenNganh = new List<string>();
        }

        public SinhVien(string maSo, string hoTen, DateTime ngaySinh, string diaChi, string lop, string hinh, bool gioiTinh, List<string> chuyenNganh)
        {
            MaSo = maSo;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            Lop = lop;
            Hinh = hinh;
            GioiTinh = gioiTinh;
            ListChuyenNganh = chuyenNganh;
        }

    }
}
