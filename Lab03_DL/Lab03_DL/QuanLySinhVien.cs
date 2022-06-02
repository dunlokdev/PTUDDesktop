using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_DL
{
    public delegate int SoSanh(object sv1, object sv2);
    class QuanLySinhVien
    {
        public List<SinhVien> DanhSachSinhVien;

        public QuanLySinhVien()
        {
            DanhSachSinhVien = new List<SinhVien>();
        }

        // Thêm một sinh viên vào danh sách
        public void ThemSinhVien(SinhVien sv)
        {
            DanhSachSinhVien.Add(sv);
        }

        public SinhVien this[int index]
        {
            get { return DanhSachSinhVien[index]; }
            set { DanhSachSinhVien[index] = value; }
        }

        // Xoá các obj trong danh sách nếu thoả điều kiện so sánh
        public void XoaSinhVien(object obj, SoSanh ss)
        {
            int i = DanhSachSinhVien.Count - 1;
            for (; i >= 0; i--)
            {
                if (ss(obj, this[i]) == 0)
                {
                    DanhSachSinhVien.RemoveAt(i);
                }
            }
        }

        // Tìm một sinh viên trong danh sách thoả điều kiện so sách
        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien svresult = null;
            foreach (SinhVien sv in DanhSachSinhVien)
            {
                if (ss(obj, sv) == 0)
                {
                    svresult = sv;
                    break;
                }
            }
            return svresult;
        }

        // Tìm một sinh viên trong danh sách thoả điều kiện so sánh,
        // gán lại thông tin cho sinh viên này thành svsua
        public bool Sua(SinhVien svsua, object obj, SoSanh ss)
        {
            int i, count;
            bool kq = false;

            count = DanhSachSinhVien.Count - 1;
            for (i = 0; i < count; i++)
            {
                if (ss(obj, this[i]) == 0)
                {
                    this[i] = svsua;
                    kq = true;
                    break;
                }
            }
            return kq;
        }

        // Hàm đọc danh sách sinh viên từ tập tin txt
        public void DocTuFile()
        {
            string filename = "data.txt", t;

            string[] str;
            SinhVien sv;

            StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open));

            while((t = sr.ReadLine()) != null)
            {
                #region Chuyển dữ liệu từ file và object SinhVien
                str = t.Split('*');
                sv = new SinhVien();

                sv.MaSo = str[0];
                sv.HoTen = str[1];
                sv.NgaySinh = DateTime.Parse(str[2]);
                sv.DiaChi = str[3];
                sv.Lop = str[4];
                sv.Hinh = str[5];
                sv.GioiTinh = str[6] == "1" ? true : false;
                string[] listChuyenNganh = str[7].Split(',');
                foreach (string chuyenNganh in listChuyenNganh)
                {
                    sv.ListChuyenNganh.Add(chuyenNganh);
                }
                #endregion

                // Thêm sinh viên vào danh sách sinh viên
                ThemSinhVien(sv);
            }

        }
    }
}
