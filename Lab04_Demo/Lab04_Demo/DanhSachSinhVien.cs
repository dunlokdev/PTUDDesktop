using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_Demo
{
    public delegate int SoSanh(object sv1, object sv2);

    public class DanhSachSinhVien
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
        #endregion

    }
}
