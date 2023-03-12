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
    public partial class ÜrünEkle : Form
    {
        public ÜrünEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FM4UKPC\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True");

        private void KategoriGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }

        private void ÜrünEkle_Load(object sender, EventArgs e)
        {
            KategoriGetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri where kategori = '"+comboBox1.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox2.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into urun(barkodno, kategori, marka, urunadi, miktari, alisfiyati, satisfiyati, tarih) values(@barkodno, @kategori, @marka, @urunadi, @miktari, @alisfiyati, @satisfiyati, @tarih)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", textBox1.Text);
            komut.Parameters.AddWithValue("@kategori", comboBox1.Text);
            komut.Parameters.AddWithValue("@marka", comboBox2.Text);
            komut.Parameters.AddWithValue("@urunadi", textBox2.Text);
            komut.Parameters.AddWithValue("@miktari", int.Parse(textBox3.Text));
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(textBox4.Text));
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(textBox5.Text));
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün eklendi.");

            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }

        }
    }
}
