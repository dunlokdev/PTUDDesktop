using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02_GiaoVien
{
    public class MonHoc
    {
        public int ID { get; set; }
        public string TenMH { get; set; }
        public int SoTC { get; set; }

        public MonHoc() { }
        public MonHoc(string ten)
        {
            this.TenMH = ten;
        }

        public MonHoc(int iD, string tenMH, int soTC) 
        {
            ID = iD;
            TenMH = tenMH;
            SoTC = soTC;
        }
        public override string ToString()
        {
            return TenMH;
        }
    }
}
