using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_Demo
{
    public delegate int SoSanh(object sv1, object sv2);

    class DanhSachSinhVien
    {
        // Fields
        public List<SinhVien> DSSV { get; set; }


        // Constructors
        public DanhSachSinhVien()
        {
            DSSV = new List<SinhVien>();
        }

        #region METHODS
        public void Them(SinhVien sv) { DSSV.Add(sv); }
        public SinhVien this[int index]
        {
            get { return DSSV[index]; }
            set { DSSV[index] = value; }
        }
        public void Xoa(object obj, SoSanh ss)
        {
            int i = DSSV.Count - 1; // 0 > length - 1
            for (; i >= 0; i--)
            {
                if (ss(obj, this[i]) == 0) DSSV.RemoveAt(i);
            }
        }
        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien svresult = null; // bien linh canh
            foreach (SinhVien sv in DSSV)
            {
                if (ss(obj, sv) == 0) // ktra neu dung la sv do thi
                {
                    svresult = sv; // gan vao bien linh canh
                    break; 
                }
            }
            return svresult;
        }
        public bool Sua(SinhVien svSua, object obj, SoSanh ss)
        {
            int i, count;
            bool kq = false;
            count = this.DSSV.Count - 1;
            for (i = 0; i < count; i++)
            {
                if (ss(obj, this[i]) == 0)
                {
                    this[i] = svSua;
                    kq = true;
                    break;
                }
             }
            return kq;
        }
        // Doc file
        public void DocTuFile()
        {
            string fileName = "dssv.txt", t;
            string[] s;
            SinhVien sv;
            StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open));
            
            while ((t = sr.ReadLine()) != null)
            {
                s = t.Split('*');
                sv = new SinhVien();
                sv.MaSo = s[0];
                sv.HoTen = s[1];
                sv.Email = s[2];
                sv.DiaChi = s[3];
                sv.NgaySinh = DateTime.Parse(s[4]);
                sv.GioiTinh = int.Parse(s[5]) == 1 ? true : false;
                sv.Lop = s[6];
                sv.SDT = s[7];

                this.Them(sv);
            }
        }
        #endregion

    }
}
