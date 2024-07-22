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
    public partial class Kategori : Form
    {
        public Kategori()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        bool durum;

        private void KategoriKontrol()
        {
            durum = true;
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select* From Kategoriler ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboKategori.Text == read["KategoriAdı"].ToString() || comboKategori.Text == "")
                {
                    durum= false;  
                }
            }
            baglanti.Close();
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            KategoriKontrol();  
            if(durum==true)
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi = "INSERT INTO Kategoriler(KategoriAdı) Values(@KategoriAdı) ";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                komut.Parameters.AddWithValue("@KategoriAdı", comboKategori.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı :)");
            }
            else
            {
                MessageBox.Show("Kategori zaten var");
            }
          
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
     
            string komutCumlesi = "DELETE FROM Kategoriler WHERE KategoriAdı = @KategoriAdı ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@KategoriAdı", comboKategori.Text);
           
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();
            baglanti.Close();
            if (etkilenenSatirSayisi > 0)
            {
                MessageBox.Show("Marka Silindi");
            }
            else
            {
                MessageBox.Show("Marka bulunamadı veya silme işlemi gerçekleştirilemedi");
            }

        }

        private void KategoriGetir()
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select * From Kategoriler";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboKategori.Items.Add(read["KategoriAdı"].ToString());
            }
            baglanti.Close();

        }

        private void Kategori_Load(object sender, EventArgs e)
        {
            KategoriGetir();
        }
    }
}
