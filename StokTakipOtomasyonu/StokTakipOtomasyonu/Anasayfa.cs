using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StokTakipOtomasyonu
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
     

        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";


        private void müşteriEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusteriEkle musterieklefrm = new MusteriEkle();
            musterieklefrm.ShowDialog();
        }

        private void müşterileriListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusteriListesi musterilistfrm = new MusteriListesi();
            musterilistfrm.ShowDialog();
        }

        private void ürünEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            UrunEkle uruneklefrm = new UrunEkle();
            uruneklefrm.ShowDialog();
        }

        private void ürünListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrunList urunlistfrm = new UrunList();
            urunlistfrm.ShowDialog();
        }

        private void kategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kategori kategorifrm = new Kategori();
            kategorifrm.ShowDialog();
        }

        private void markaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Marka markafrm = new Marka();
            markafrm.ShowDialog();
        }

        private void siparişleriListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SatıslarıList satıslarıListfrm = new SatıslarıList();
            satıslarıListfrm.ShowDialog();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void SepetListele()
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select * From Sepet";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            baglanti.Close();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            SepetListele();
        }

        private void textTc_TextChanged(object sender, EventArgs e)
        {
            if (textTc.Text == "")
            {
                textAdSoyad.Clear();
                textTelefon.Clear();
            }

            else
            {
                string tcAra = textTc.Text;
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();

                string komutCumlesi = "SELECT * FROM Musteriler WHERE Tc LIKE '%" + tcAra + "%'";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    textAdSoyad.Text = read["AdSoyad"].ToString();
                    textTelefon.Text = read["Telefon"].ToString();

                }

                baglanti.Close();
            }
        }

        private void textBarkod_TextChanged(object sender, EventArgs e)
        {
            if (textBarkod.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != textMiktar)
                        {
                            item.Text = "";
                        }
                    }

                }
            }

            else
            {
                string barkodAra = textBarkod.Text;
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();

                string komutCumlesi = "SELECT * FROM Urunler WHERE BarkodNO LIKE '%" + barkodAra + "%'";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    textUrunAd.Text = read["UrunAdı"].ToString();
                    textSatısFiyat.Text = read["SatısFiyat"].ToString();

                }

                baglanti.Close();
            }
        }

        bool durumKontrol;
        private void BarkodKontrol()
        { 
            durumKontrol = true;    
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCümlesi = "Select * From Sepet";
            SqlCommand komut = new SqlCommand(komutCümlesi, baglanti);
            SqlDataReader read =komut.ExecuteReader();
            while (read.Read())
            {
                if (int.Parse(read["BarkodNO"].ToString()) == int.Parse(textBarkod.Text))
                {
                    durumKontrol = false;
                    
                }
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            try
            {
                BarkodKontrol();
                if (durumKontrol == true)
                {
                    SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                    baglanti.Open();
                    string komutCumlesi = "INSERT INTO Sepet Values(@Tc,@AdSoyad,@Telefon,@BarkodNO,@UrunAdı,@Miktar,@SatısFiyat,@ToplamFiyat,@Tarih) ";
                    SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

                    komut.Parameters.AddWithValue("@Tc", textTc.Text);
                    komut.Parameters.AddWithValue("@AdSoyad", textAdSoyad.Text);
                    komut.Parameters.AddWithValue("@Telefon", textTelefon.Text);
                    komut.Parameters.AddWithValue("@BarkodNO", textBarkod.Text);
                    komut.Parameters.AddWithValue("@UrunAdı", textUrunAd.Text);
                    komut.Parameters.AddWithValue("@Miktar", int.Parse(textMiktar.Text));
                    komut.Parameters.AddWithValue("@SatısFiyat", decimal.Parse(textSatısFiyat.Text));
                    komut.Parameters.AddWithValue("@ToplamFiyat", decimal.Parse(textToplam.Text));
                    komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString("yyyy-MM-dd"));


                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }
                else
                {
                    SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                    baglanti.Open();

                    SqlCommand komut2 = new SqlCommand("UPDATE Sepet SET Miktar = Miktar + @Miktar WHERE BarkodNO = @BarkodNO", baglanti);
                    komut2.Parameters.AddWithValue("@Miktar", int.Parse(textMiktar.Text));
                    komut2.Parameters.AddWithValue("@BarkodNO", textBarkod.Text);
                    int etkilenenSatirSayisi = komut2.ExecuteNonQuery();

                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Miktar alanı güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme işlemi başarısız oldu.");
                    }

                    SqlCommand komut3 = new SqlCommand("UPDATE Sepet SET ToplamFiyat = Miktar * SatısFiyat WHERE BarkodNO = @BarkodNO", baglanti);
                    komut3.Parameters.AddWithValue("@BarkodNO", textBarkod.Text);
                    komut3.ExecuteNonQuery();
                  

                    baglanti.Close();
                }
                textMiktar.Text = "1";
                dataGridView1.DataSource = null;
              
                SepetListele();
                Hesapla();

                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != textMiktar)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("HATA");

            }

        }
         

        private void textMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textToplam.Text = (double.Parse(textMiktar.Text) * double.Parse(textSatısFiyat.Text)).ToString();
            }
            catch (Exception  ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.ToString());
                MessageBox.Show("Hata meydana geldiği satır: " + ex.StackTrace);

            }
        }

        private void textSatısFiyat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textToplam.Text = (double.Parse(textMiktar.Text) * double.Parse(textSatısFiyat.Text)).ToString();
            }
            catch (Exception ex )
            {
                Console.WriteLine("Hata oluştu: " + ex.ToString());
                Console.WriteLine("Hata meydana geldiği satır: " + ex.StackTrace);

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi = "Delete From Sepet Where BarkodNO='" + dataGridView1.CurrentRow.Cells["BarkodNO"].Value.ToString() + "'";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                komut.ExecuteNonQuery();

                baglanti.Close();
                dataGridView1.DataSource = null;
                MessageBox.Show("Ürün Sepetten silindi ");
                SepetListele();
                Hesapla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                    MessageBox.Show("Hata meydana geldiği satır: " + ex.StackTrace);
            }
            
        }

        private void btnSatısIptal_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(" Tüm Sepeti silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            { 

             SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Delete From Sepet ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            dataGridView1.DataSource = null;
            MessageBox.Show("Satıp İptal BAŞARILI :)");
            SepetListele();
                Hesapla();
            }
        }

        private void Hesapla()
        {
            try
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi = "Select SUM(ToplamFiyat) From Sepet ";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                decimal toplamFiyat = (decimal)komut.ExecuteScalar();
                labelGenel.Text = toplamFiyat.ToString("N2") + " TL";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                MessageBox.Show("Hata meydana geldiği satır: " + ex.StackTrace);
            }
        }

        private void btnSatısYap_Click(object sender, EventArgs e)
        {
            try
          {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {

                    SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                    baglanti.Open();
                    string komutCumlesi = "INSERT INTO Satıs Values(@Tc,@AdSoyad,@Telefon,@BarkodNO,@UrunAdı,@Miktar,@SatısFiyat,@ToplamFiyat,@Tarih) ";
                    SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

                    komut.Parameters.AddWithValue("@Tc", textTc.Text);
                    komut.Parameters.AddWithValue("@AdSoyad", textAdSoyad.Text);
                    komut.Parameters.AddWithValue("@Telefon", textTelefon.Text);
                    komut.Parameters.AddWithValue("@BarkodNO", dataGridView1.Rows[i].Cells["BarkodNO"].Value.ToString());
                    komut.Parameters.AddWithValue("@UrunAdı", dataGridView1.Rows[i].Cells["UrunAdı"].Value.ToString());
                    komut.Parameters.AddWithValue("@Miktar", int.Parse(dataGridView1.Rows[i].Cells["Miktar"].Value.ToString()));
                    komut.Parameters.AddWithValue("@SatısFiyat", decimal.Parse(dataGridView1.Rows[i].Cells["SatısFiyat"].Value.ToString()));
                    komut.Parameters.AddWithValue("@ToplamFiyat", decimal.Parse(dataGridView1.Rows[i].Cells["ToplamFiyat"].Value.ToString()));
                    komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString("yyyy-MM-dd"));

                    komut.ExecuteNonQuery();


                    string komutCumlesi2 = "UPDATE Urunler SET Miktar = Miktar - @Miktar WHERE BarkodNO = @BarkodNO";
                    SqlCommand komut2 = new SqlCommand(komutCumlesi2, baglanti);
                    komut2.Parameters.AddWithValue("@BarkodNO", dataGridView1.Rows[i].Cells["BarkodNO"].Value.ToString());
                    int satilanMiktar = int.Parse(dataGridView1.Rows[i].Cells["Miktar"].Value.ToString());
                    komut2.Parameters.AddWithValue("@Miktar", satilanMiktar);
                    komut2.ExecuteNonQuery();

                    baglanti.Close();
                    //sepeti temizleme 
                    SqlConnection baglanti2 = new SqlConnection(baglantiCumlesi);
                    baglanti2.Open();
                    string komutCumlesi3 = "DELETE FROM Sepet WHERE BarkodNO = @BarkodNO";
                    SqlCommand komut3 = new SqlCommand(komutCumlesi3, baglanti2);
                    komut3.Parameters.AddWithValue("@BarkodNO", dataGridView1.Rows[i].Cells["BarkodNO"].Value.ToString());
                    komut3.ExecuteNonQuery();
                    baglanti2.Close();
                    MessageBox.Show("Satış Yapıldı");

                }
           
               
           }
           catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                MessageBox.Show("Hata meydana geldiği satır: " + ex.StackTrace);

            }
            dataGridView1.DataSource = null;
            SepetListele();
            Hesapla();
        }
    }
}
    

   
    
