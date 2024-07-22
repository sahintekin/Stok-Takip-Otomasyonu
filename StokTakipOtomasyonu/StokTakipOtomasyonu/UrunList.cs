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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace StokTakipOtomasyonu
{
    public partial class UrunList : Form
    {
        public UrunList()
        {
            InitializeComponent();
        }

        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";

        private void UrunListele()
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select * From Urunler";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
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




        private void UrunList_Load(object sender, EventArgs e)
        {
            UrunListele();
            KategoriGetir();
        }


        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (mtextBarkodno.Text != "")
                {

                    SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                    baglanti.Open();

                    string komutCumlesi = "UPDATE Urunler SET KategoriAdı=@KategoriAdı , Marka=@Marka,UrunAdı=@UrunAdı,  Miktar =@Miktar, AlısFiyat=@AlısFiyat, SatısFiyat=@SatısFiyat  WHERE BarkodNO=@BarkodNO";

                    SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                    komut.Parameters.AddWithValue("@BarkodNO", mtextBarkodno.Text);
                    if (comboKategori.SelectedItem != null)
                    {
                        komut.Parameters.AddWithValue("@KategoriAdı", comboKategori.SelectedItem.ToString());
                    }
                   

                    if (comboMarka.SelectedItem != null)
                    {
                        komut.Parameters.AddWithValue("@Marka", comboMarka.SelectedItem.ToString());
                    }

                    komut.Parameters.AddWithValue("@UrunAdı", txtUrunad.Text);
                    komut.Parameters.AddWithValue("@Miktar", int.Parse(txtMiktar.Text));
                    komut.Parameters.AddWithValue("@AlısFiyat", double.Parse(txtAlıs.Text));
                    komut.Parameters.AddWithValue("@SatısFiyat", double.Parse(txtSatıs.Text));
                    komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString());

                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    UrunListele();
                    MessageBox.Show("Güncellendi ");
                }
                else
                {
                    MessageBox.Show(" Hatalı işlem Güncellenemedi ");
                }
                

            }
            catch (SqlException ex)
            {

                MessageBox.Show("Hata Oluştu: " + ex.Message);
            }

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
              mtextBarkodno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboKategori.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboMarka.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtUrunad.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
              txtMiktar.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
              txtAlıs.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
              txtSatıs.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            

                  
            }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "Delete From Urunler Where BarkodNO='" + dataGridView1.CurrentRow.Cells["BarkodNO"].Value.ToString() + "'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            UrunListele();
            MessageBox.Show("KAYIT SİLME BAŞARILI :)");
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

       

        private void mbarkodnoara_TextChanged(object sender, EventArgs e)
        {
            string barkodAra = mbarkodnoara.Text;
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();
            string komutCumlesi = "SELECT * FROM Urunler WHERE BarkodNO LIKE '%" + barkodAra + "%'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            baglanti.Close();
            UrunListele();
            dataGridView1.DataSource = dt;
        }
    }
    }

