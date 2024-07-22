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
    public partial class MusteriListesi : Form
    {
        public MusteriListesi()
        {
            InitializeComponent();
        }
        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        private void MusteriList_Load(object sender, EventArgs e)
        {
            Musteri_Listele();
        }
        public void Musteri_Listele()
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select * From Musteriler";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            baglanti.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mtxtTc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            mtxtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Update Musteriler set AdSoyad=@AdSoyad,Telefon=@Telefon,Email=@Email,Adres=@Adres Where Tc=@Tc ";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@Tc", mtxtTc.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", mtxtTelefon.Text);
            komut.Parameters.AddWithValue("@Email", txtEmail.Text);     
            komut.Parameters.AddWithValue("@Adres", txtAdres.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Musteri_Listele();
            MessageBox.Show("KAYIT GÜNCELLEME BAŞARILI:)");
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            Musteri_Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Delete From Musteriler Where Tc='" + dataGridView1.CurrentRow.Cells["Tc"].Value.ToString() + "'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Musteri_Listele();
            MessageBox.Show("KAYIT SİLME BAŞARILI :)");
        }

        private void mtxtTcAra_TextChanged(object sender, EventArgs e)
        {

            string tcAra = mtxtTcAra.Text;
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "SELECT * FROM Musteriler WHERE Tc LIKE '%" + tcAra + "%'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader); 
            reader.Close(); 
            baglanti.Close();
            Musteri_Listele();
            dataGridView1.DataSource = dt;
        }

        
    }
}
