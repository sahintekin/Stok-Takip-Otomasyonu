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
using System.Data.SqlClient;
using System.Security.Policy;

namespace StokTakipOtomasyonu
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";


       private void btnGiris_Click(object sender, EventArgs e)
        {

            string komutCümlesi = "SELECT COUNT(*) FROM Calisanlar WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";

                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);

                baglanti.Open();

                SqlCommand komut = new SqlCommand(komutCümlesi, baglanti);
                komut.Parameters.AddWithValue("@KullaniciAdi", txtKullanici.Text);
                komut.Parameters.AddWithValue("@Sifre", int.Parse(maskedSifre.Text));

                int kayitSayisi = (int)komut.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    string unvan = GetCalisanUnvan(txtKullanici.Text); 
                    MessageBox.Show("Kullanıcı girişi başarılı! Hoş Geldiniz! " + unvan);

                    Anasayfa anasayfafrm = new Anasayfa();
                    anasayfafrm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Geçersiz kullanıcı adı veya şifre!");
                }
            }
        

        private string GetCalisanUnvan(string kullaniciAdi)
        {
            string unvan;

            string unvanSorgusu = "SELECT Unvan FROM Calisanlar WHERE KullaniciAdi = @KullaniciAdi";
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            
                baglanti.Open();

                SqlCommand komut = new SqlCommand(unvanSorgusu, baglanti);
                komut.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                unvan = (string)komut.ExecuteScalar();
            

            return unvan;
        }


        

        private void btnKaydol_Click(object sender, EventArgs e)
        {
  
            try
               {
                        
                 string komutCümlesi = "INSERT INTO Calisanlar (Unvan, AdiSoyadi, Yasi, BaslamaTarihi, KullaniciAdi, Sifre) VALUES (@Unvan, @AdiSoyadi, @Yasi, @BaslamaTarihi, @KullaniciAdi, @Sifre)";

                 SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                 baglanti.Open();
                 SqlCommand komut = new SqlCommand(komutCümlesi, baglanti);

              
                            komut.Parameters.AddWithValue("@Unvan", txtUnvan.Text);
                            komut.Parameters.AddWithValue("@AdiSoyadi", txtAdiSoyadi.Text);
                            komut.Parameters.AddWithValue("@Yasi", int.Parse(mtxtYasi.Text));
                            komut.Parameters.AddWithValue("@BaslamaTarihi",DateTime.Parse( mtxtBaslamaTarihi.Text));
                            komut.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciKayit.Text);
                            komut.Parameters.AddWithValue("@Sifre", int.Parse(mtxtSifre.Text));
                            komut.ExecuteNonQuery();
                            MessageBox.Show("Kaydolma işlemi başarılı!"); 
                        }
                      catch (Exception ex)
                        {

                        MessageBox.Show("Kaydolma işlemi sırasında bir hata oluştu: " + ex.Message);
                        
                         }
                }

      

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtKullanici.Text = string.Empty;
            maskedSifre.Text = string.Empty;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            txtUnvan.Clear();
            txtAdiSoyadi.Clear();
            mtxtYasi.Clear();
            mtxtBaslamaTarihi.Clear();
            txtKullaniciKayit.Clear();
            mtxtSifre.Clear();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifreYenileme sifredeğiştirfrm = new SifreYenileme();
            sifredeğiştirfrm.ShowDialog();
           
        }
    }

  }




