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
    public partial class frnThemMH : Form
    {
        private List<LoaiMatHang> listLMH;
        public frnThemMH(List<LoaiMatHang> list)
        {
            listLMH = list;
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Du lieu chua duoc nhap, vui long kiem tra lai!");
                return;
            }
            string connectionString = @"Data Source=DUNLOC\SQLEXPRESS;Initial Catalog=QLBANHANG_De3;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand($"INSERT LOAIMATHANG VALUES (N'{txtTenLoai.Text}')", connection);
            connection.Open();
            var result = command.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Them thanh cong!");

            }
            else
            {
                MessageBox.Show("That bai!");
            }
            connection.Close();
            Close();
        }
    }
}
