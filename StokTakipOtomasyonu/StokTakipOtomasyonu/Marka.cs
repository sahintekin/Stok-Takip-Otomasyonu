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
    public partial class Marka : Form
    {
        public Marka()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        bool durum;

        private void MarkaKontrol()
        {
            durum = true;
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Select* From Markalar ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboKategori.Text == read["KategoriAdı"].ToString() && comboMarka.Text == read["Marka"].ToString() || comboMarka.Text == "" || comboKategori.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            MarkaKontrol();
            if (durum==true)
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi = "INSERT INTO Markalar Values(@KategoriAdı,@Marka) ";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                komut.Parameters.AddWithValue("@KategoriAdı", comboKategori.Text);
                komut.Parameters.AddWithValue("@Marka", comboMarka.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı :)");
            }
            else
            {
                MessageBox.Show("Marka ve Kategori zaten var");
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
     
        

        private void Marka_Load(object sender, EventArgs e)
        {
           KategoriGetir();

        }

        
           
    }
}
