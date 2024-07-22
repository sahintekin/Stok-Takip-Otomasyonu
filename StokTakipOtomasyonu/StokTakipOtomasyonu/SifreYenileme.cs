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

namespace StokTakipOtomasyonu
{
    public partial class SifreYenileme : Form
    {
        public SifreYenileme()
        {
            InitializeComponent();
        }

        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textKullanici.Text;
            string Sifre = maskedTextSifre.Text;

            try
            {
                string komutCümlesi = "UPDATE Calisanlar SET Sifre = @Sifre WHERE KullaniciAdi = @KullaniciAdi";

                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                
                    baglanti.Open();

                SqlCommand komut = new SqlCommand(komutCümlesi, baglanti);
                    
                       komut.Parameters.AddWithValue("@Sifre", int.Parse(Sifre));
                       komut.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                        int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                        if (etkilenenSatirSayisi > 0)
                        {
                            MessageBox.Show("Şifre sıfırlama işlemi başarılı! Yeni şifreniz kaydedildi.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı bulunamadı!");
                        }

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Şifre sıfırlama işlemi sırasında bir hata oluştu: " + ex.Message);
            }
        }
    }
}
