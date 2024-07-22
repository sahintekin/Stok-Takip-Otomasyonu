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
    public partial class UrunEkle : Form
    {
        public UrunEkle()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        bool durum;

        private void BarkodNoKontrol()
        {
            durum = true;
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select* From Urunler ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (mtextBarkodno.Text == read["BarkodNo"].ToString() || mtextBarkodno.Text == "" )
                {
                    durum = false;
                }
            }
            baglanti.Close();
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
        private void UrunEkle_Load(object sender, EventArgs e)
        {
            KategoriGetir();
        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select * From Markalar where KategoriAdı=@KategoriAdı";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@KategoriAdı", comboKategori.Text);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["Marka"].ToString());
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            BarkodNoKontrol();
            if (durum == true)
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi = "INSERT INTO Urunler Values(@BarkodNo,@KategoriAdı,@Marka,@UrunAdı,@Miktar,@AlısFiyat,@SatısFiyat,@Tarih) ";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

                komut.Parameters.AddWithValue("@BarkodNo", mtextBarkodno.Text);
                komut.Parameters.AddWithValue("@KategoriAdı", comboKategori.Text);
                komut.Parameters.AddWithValue("@Marka", comboMarka.Text);
                komut.Parameters.AddWithValue("@UrunAdı", txtUrunad.Text);
                komut.Parameters.AddWithValue("@Miktar", int.Parse(txtMiktar.Text));
                komut.Parameters.AddWithValue("@AlısFiyat", double.Parse(txtAlıs.Text));
                komut.Parameters.AddWithValue("@SatısFiyat", double.Parse(txtSatıs.Text));
                komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı :)");
            }

            else
            {
                MessageBox.Show("BarkodNo zaten var");
            }



         
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

                else if (item is ComboBox)
                {
                    item.Text = "";
                }
                else if (item is MaskedTextBox) { item.Text = ""; }
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtBarkod.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {

                    labelMiktar.Text = "";
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            if (txtBarkod.Text != "")
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi = "SELECT * FROM Urunler WHERE BarkodNo LIKE '%" + txtBarkod.Text + "%'";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                SqlDataReader read = komut.ExecuteReader();

                while (read.Read())
                {
                    texKategori.Text = read["KategoriAdı"].ToString();
                    texMarka.Text = read["Marka"].ToString();
                    texUrun.Text = read["UrunAdı"].ToString();
                    texAlıs.Text = read["AlısFiyat"].ToString();
                    texSatıs.Text = read["SatısFiyat"].ToString();
                    labelMiktar.Text = read["Miktar"].ToString();
                }
                 
                baglanti.Close();
            }
            else
            {
        
                texKategori.Clear();
                texMarka.Clear();
                texUrun.Clear();
                texAlıs.Clear();
                texSatıs.Clear();
                labelMiktar.Text = "";
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Update Urunler set Miktar=Miktar +@Miktar ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@Miktar", int.Parse(textMiktar.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach(Control item in groupBox2.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("var olan ürüne eklendi!");
        }
    }
    }

    

