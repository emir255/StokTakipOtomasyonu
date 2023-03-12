using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StokTakipOtomasyonu
{
    public partial class MüsteriEkle : Form
    {
        public MüsteriEkle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FM4UKPC\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True");
        private void MüsteriEkle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into musteri(tc, adsoyad, telefon, adres, email) values(@tc, @adsoyad, @telefon, @adres, @email)", baglanti);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@telefon", textBox3.Text);
            komut.Parameters.AddWithValue("@adres", textBox4.Text);
            komut.Parameters.AddWithValue("@email", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri kaydı başarıyla eklenmiştir.");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }
    }
}
