using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnThiDe3
{
    public partial class frmQuanLyBanHang : Form
    {
        private const string _connectionString = @"Data Source=DUNLOC\SQLEXPRESS;Initial Catalog=QLBANHANG_De3;Integrated Security=True";
        private List<MatHang> _listMH;
        private List<LoaiMatHang> _listLMH;
        private MatHang mhCurrent;
        public frmQuanLyBanHang()
        {
            InitializeComponent();
            _listMH = new List<MatHang>();
            _listLMH = new List<LoaiMatHang>();
            mhCurrent = new MatHang();
        }

        private void frmQuanLyBanHang_Load(object sender, EventArgs e)
        {
            _listMH = GetMatHangInDB();
            _listLMH = GetLoaiMatHangInDB();
            DisplayMatHang(_listMH);
            DisplayComboxLoaiMatHang(_listLMH);
        }

        private void DisplayComboxLoaiMatHang(List<LoaiMatHang> list)
        {
            cbLoaiMatHang.DataSource = list;
            cbLoaiMatHang.DisplayMember = "TenLoai";
            cbLoaiMatHang.ValueMember = "MaLoai";
        }

        private List<MatHang> GetMatHangInDB()
        {
            List<MatHang> list = new List<MatHang>();
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM MATHANG", connection);
            connection.Open();
            var exec = command.ExecuteReader();
            while (exec.Read())
            {
                MatHang mh = new MatHang();
                mh.MaMatHang = Convert.ToInt32(exec["MaMatHang"]);
                mh.TenMatHang = exec["TenMatHang"].ToString();
                mh.LoaiMatHang = Convert.ToInt32(exec["LoaiMatHang"]);
                mh.SoLuongTon = Convert.ToInt32(exec["SoLuongTon"]);
                mh.DonGia = Convert.ToInt32(exec["DonGia"]);
                list.Add(mh);
            }
            connection.Close();
            connection.Dispose();

            return list;
        }
        private List<LoaiMatHang> GetLoaiMatHangInDB()
        {
            List<LoaiMatHang> list = new List<LoaiMatHang>();
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM LOAIMATHANG", connection);
            connection.Open();
            var exec = command.ExecuteReader();
            while (exec.Read())
            {
                LoaiMatHang lmh = new LoaiMatHang();
                lmh.MaLoai = Convert.ToInt32(exec["MaLoai"]);
                lmh.TenLoai = exec["TenLoai"].ToString();

                list.Add(lmh);
            }
            connection.Close();
            connection.Dispose();

            return list;
        }
        private void DisplayMatHang(List<MatHang> listMH)
        {
            lvMatHang.Items.Clear();
            foreach (var item in listMH)
            {
                //ListViewItem lvitem = new ListViewItem(item.MaMatHang.ToString());
                //lvitem.SubItems.Add(item.TenMatHang);
                //string lmh = _listLMH.Find(i => i.MaLoai == item.LoaiMatHang).TenLoai;
                //lvitem.SubItems.Add(lmh);
                //lvitem.SubItems.Add(item.SoLuongTon.ToString());
                //lvitem.SubItems.Add(item.DonGia.ToString());

                string lmh = _listLMH.Find(i => i.MaLoai == item.LoaiMatHang).TenLoai;
                string[] row = { item.MaMatHang.ToString(), item.TenMatHang, lmh, item.SoLuongTon.ToString(), item.DonGia.ToString() };

                //lvMatHang.Items.Add(lvitem);
                ListViewItem lvitem = new ListViewItem(row);
                lvMatHang.Items.Add(lvitem);
            }
        }

        private void lvMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = lvMatHang.SelectedItems.Count;
            if (count > 0)
            {
                int id = Convert.ToInt32(lvMatHang.SelectedItems[0].Text);
                MatHang mh = _listMH.FirstOrDefault(item => item.MaMatHang == id);
                ThietLapThongTinControl(mh);
            }
        }
        private void ThietLapThongTinControl(MatHang mh)
        {
            txtMaMatHang.Text = mh.MaMatHang.ToString();
            txtTenMatHang.Text = mh.TenMatHang.ToString();
            nudSoLuong.Text = mh.SoLuongTon.ToString();
            nudDonGia.Text = mh.DonGia.ToString();
            cbLoaiMatHang.SelectedIndex = _listLMH.FindIndex(item => item.MaLoai == mh.LoaiMatHang);
        }

        private MatHang GetDuLieuControls()
        {
            if (String.IsNullOrWhiteSpace(txtTenMatHang.Text)) return null;
            MatHang mh = new MatHang();
            mh.MaMatHang = String.IsNullOrWhiteSpace(txtMaMatHang.Text) ? -1 : Convert.ToInt32(txtMaMatHang.Text);
            mh.TenMatHang = txtTenMatHang.Text;
            mh.SoLuongTon = Convert.ToInt32(nudSoLuong.Value);
            mh.DonGia = Convert.ToInt32(nudDonGia.Value);
            mh.LoaiMatHang = Convert.ToInt32(cbLoaiMatHang.SelectedValue);
            return mh;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            MatHang mh = GetDuLieuControls();
            if (mh == null)
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin, vui lòng kiểm tra lại!");
                return;
            }
            SqlConnection connection = new SqlConnection(_connectionString);
            string queryInsert = $"INSERT dbo.MATHANG (TenMatHang,LoaiMatHang,SoLuongTon,DonGia) VALUES(N'{mh.TenMatHang}',{mh.LoaiMatHang},{mh.SoLuongTon}, {mh.DonGia})";
            SqlCommand command = new SqlCommand(queryInsert, connection);

            connection.Open();
            var result = command.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("Thanh cong!");
                _listMH = GetMatHangInDB();
                DisplayMatHang(_listMH);
            }
            else
                MessageBox.Show("That bai!");
            connection.Close();

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            MatHang mh = GetDuLieuControls();
            if (mh == null)
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin, vui lòng kiểm tra lại!");
                return;
            }
            SqlConnection connection = new SqlConnection(_connectionString);
            string queryUpdate = $"UPDATE dbo.MATHANG " +
                $"SET TenMatHang = N'{mh.TenMatHang}', LoaiMatHang = {mh.LoaiMatHang}, SoLuongTon = {mh.SoLuongTon}, DonGia = {mh.DonGia}" +
                $" WHERE MaMatHang = {mh.MaMatHang}";
            SqlCommand command = new SqlCommand(queryUpdate, connection);

            connection.Open();
            var result = command.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("Thanh cong!");
                _listMH = GetMatHangInDB();
                DisplayMatHang(_listMH);
            }
            else
                MessageBox.Show("That bai!");
            connection.Close();
        }

        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            frnThemMH form = new frnThemMH(_listLMH);
            form.ShowDialog();
            form.FormClosed += (s, args) => {
                _listLMH = GetLoaiMatHangInDB();
                DisplayComboxLoaiMatHang(_listLMH); 
            };
        }
    }
}
