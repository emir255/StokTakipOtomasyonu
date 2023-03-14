using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
