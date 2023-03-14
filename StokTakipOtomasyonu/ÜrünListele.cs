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
    public partial class ÜrünListele : Form
    {
        public ÜrünListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FM4UKPC\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True");
        DataSet ds = new DataSet();
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
        private void ÜrünListele_Load(object sender, EventArgs e)
        {
            UrunListele();
            KategoriGetir();
        }

        private void UrunListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun", baglanti);
            adtr.Fill(ds, "urun");
            dataGridView1.DataSource = ds.Tables["urun"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set urunadi = @urunadi, miktari = @miktari, alisfiyati = @alisfiyati, satisfiyati = @satisfiyati where barkodno = @barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", textBox2.Text);
            komut.Parameters.AddWithValue("@urunadi", textBox5.Text);
            komut.Parameters.AddWithValue("@miktari", int.Parse(textBox6.Text));
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(textBox7.Text));
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(textBox8.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["urun"].Clear();
            UrunListele();
            MessageBox.Show("Güncelleme yapıldı.");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set kategori = @kategori, marka = @marka where barkodno = @barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", textBox2.Text);
                komut.Parameters.AddWithValue("@kategori", comboBox1.Text);
                komut.Parameters.AddWithValue("@marka", comboBox2.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();
                ds.Tables["urun"].Clear();
                UrunListele();
                MessageBox.Show("Güncelleme yapıldı.");
                
            }
            else
            {
                MessageBox.Show("Barkod no Boş geçilemez!");
            }
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri where kategori = '" + comboBox1.SelectedItem + "'", baglanti);
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
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Tables["urun"].Clear();
            UrunListele();
            MessageBox.Show("Kayıt Silindi.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun where barkodno like '%" + textBox1.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
