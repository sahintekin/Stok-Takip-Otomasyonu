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
    public partial class MusteriEkle : Form
    {
        public MusteriEkle()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        private void MusteriEkle_Load(object sender, EventArgs e)
        {

        }

        private void btnEkleme_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "INSERT INTO Musteriler Values(@Tc,@AdSoyad,@Telefon,@Email,@Adres) ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

            komut.Parameters.AddWithValue("@Tc", mtxtTc.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", mtxtTelefon.Text);
            komut.Parameters.AddWithValue("@Email", txtEmail.Text);
            komut.Parameters.AddWithValue("Adres", txtAdres.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarılı :)");
        }

        private void mtxtTc_DoubleClick(object sender, EventArgs e)
        {
            mtxtTc.Text = string.Empty;
            txtAdSoyad.Text = string.Empty;
            txtEmail.Text = string.Empty;
            mtxtTelefon.Text = string.Empty;
            txtAdres.Text = string.Empty;
        }

        private void mtxtTc_TextChanged(object sender, EventArgs e)
        {
            string text = mtxtTc.Text;

            if (text == "0")
            {
                MessageBox.Show("Geçersiz değer! 0 ile başlayamaz.");
                mtxtTc.Text = "";
            }
        }
    }
}
