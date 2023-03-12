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
    public partial class MüsteriListele : Form
    {
        public MüsteriListele()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FM4UKPC\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet ds = new DataSet();

        private void MüsteriListele_Load(object sender, EventArgs e)
        {
            KayıtGöster();
        }

        private void KayıtGöster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from musteri", baglanti);
            adtr.Fill(ds, "musteri");
            dataGridView1.DataSource = ds.Tables["musteri"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update musteri set adsoyad=@adsoyad, telefon=@telefon, adres=@adres, email=@email where tc=@tc ", baglanti);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@telefon", textBox3.Text);
            komut.Parameters.AddWithValue("@adres", textBox4.Text);
            komut.Parameters.AddWithValue("@email", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["musteri"].Clear();
            KayıtGöster();
            MessageBox.Show("Müşteri kaydı başarıyla güncellendi.");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musteri where tc='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() +"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["musteri"].Clear();
            KayıtGöster();
            MessageBox.Show("Kayıt Silindi.");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from musteri where tc like '%"+textBox6.Text+"%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
