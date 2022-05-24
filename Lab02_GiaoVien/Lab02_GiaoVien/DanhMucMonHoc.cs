using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02_GiaoVien
{
    public class DanhMucMonHoc
    {
        public ArrayList ds;

        public DanhMucMonHoc() { ds = new ArrayList(); }
        public MonHoc this[int index]
        {
            get { return ds[index] as MonHoc;  }
            set { ds[index] = value; }
        }
        public void Them(MonHoc mh)
        {
            ds.Add(mh);
        }
        public override string ToString()
        {
            string str = "Danh sach mon hoc: ";
            foreach (MonHoc mh in ds)
            {
                str += mh as MonHoc + ";";
            }
            return str;
        }
    }
}
