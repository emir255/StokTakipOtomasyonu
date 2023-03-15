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

namespace StokTakipOtomasyonu
{
    public partial class SatisListele : Form
    {
        public SatisListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FM4UKPC\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet ds = new DataSet();

        private void satisListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet", baglanti);
            adtr.Fill(ds, "sepet");
            dataGridView1.DataSource = ds.Tables["sepet"];
            
            baglanti.Close();
        }

        private void SatisListele_Load(object sender, EventArgs e)
        {
            satisListele();
        }
    }
}
