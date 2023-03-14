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
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FM4UKPC\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet ds = new DataSet();
        bool durum;
        private void BarkodKontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from sepet", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox4.Text == read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
        }

        private void SepetListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet", baglanti);
            adtr.Fill(ds, "sepet");
            dataGridView1.DataSource = ds.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MüsteriEkle MüsteriEkle = new MüsteriEkle();
            MüsteriEkle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MüsteriListele MüsteriListele = new MüsteriListele();
            MüsteriListele.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ÜrünEkle ÜrünEkle = new ÜrünEkle();
            ÜrünEkle.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Kategori Kategori = new Kategori();
            Kategori.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Marka Marka = new Marka();
            Marka.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ÜrünListele ÜrünListele = new ÜrünListele();
            ÜrünListele.ShowDialog();
        }

        private void Satis_Load(object sender, EventArgs e)
        {
            SepetListele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox2.Text = "";
                textBox3.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from musteri where tc like '"+textBox1.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["adsoyad"].ToString();
                textBox3.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno like '" + textBox4.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox5.Text = read["urunadi"].ToString();
                textBox7.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void Temizle()
        {
            if (textBox4.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != textBox6)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BarkodKontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(tc, adsoyad, telefon, barkodno, urunadi, miktari, satisfiyati, toplamfiyati, tarih) values(@tc, @adsoyad, @telefon, @barkodno, @urunadi, @miktari, @satisfiyati, @toplamfiyati, @tarih)", baglanti);
                komut.Parameters.AddWithValue("tc", textBox1.Text);
                komut.Parameters.AddWithValue("adsoyad", textBox2.Text);
                komut.Parameters.AddWithValue("telefon", textBox3.Text);
                komut.Parameters.AddWithValue("barkodno", textBox4.Text);
                komut.Parameters.AddWithValue("urunadi", textBox5.Text);
                komut.Parameters.AddWithValue("miktari", int.Parse(textBox6.Text));
                komut.Parameters.AddWithValue("satisfiyati", double.Parse(textBox7.Text));
                komut.Parameters.AddWithValue("toplamfiyati", double.Parse(textBox8.Text));
                komut.Parameters.AddWithValue("tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update sepet miktari = miktari + '"+int.Parse(textBox6.Text)+"' where barkodno = '" + textBox4.Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                SqlCommand komut3 = new SqlCommand("update sepet toplamfiyat = miktari * satisfiyati where barkodno = '"+textBox4.Text+"' ", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            
            textBox6.Text = "1";
            ds.Tables["sepet"].Clear();
            SepetListele();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != textBox6)
                    {
                        item.Text = "";
                    }
                }

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox8.Text = (double.Parse(textBox6.Text) * double.Parse(textBox7.Text)).ToString();
            }
            catch (Exception)
            {

                
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox8.Text = (double.Parse(textBox6.Text) * double.Parse(textBox7.Text)).ToString();
            }
            catch (Exception)
            {


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno = '" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() +"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün sepetten çıkarıldı.");
            ds.Tables["sepet"].Clear();
            SepetListele();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürünler sepetten çıkarıldı.");
            ds.Tables["sepet"].Clear();
            SepetListele();
        }
    }
}
