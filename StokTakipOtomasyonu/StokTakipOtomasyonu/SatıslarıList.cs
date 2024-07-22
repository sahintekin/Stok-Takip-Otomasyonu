using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
namespace StokTakipOtomasyonu
{
    public partial class SatıslarıList : Form
    {
        public SatıslarıList()
        {
            InitializeComponent();
        }

        private string baglantiCumlesi = @"Data Source=AHMET\SQLEXPRESS;Initial Catalog=StokTakip;Integrated Security=True";


        private void SatısListele()
        {
            SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
            baglanti.Open();

            string komutCumlesi = "Select * From Satıs";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = true;
            baglanti.Close();
        }

        private void SatıslarıList_Load(object sender, EventArgs e)
        {
            SatısListele();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        if (dataGridView1[j, i].Value is DateTime dateValue)
                        {
                            myRange.NumberFormat = "dd.MM.yyyy"; 
                            myRange.Value2 = dateValue;
                        }
                        else
                        {
                            myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        }
                        myRange.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel dosyasına aktarılırken bir hata oluştu: " + ex.Message);
            }



        }
    

        private void btnSatısSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(" Tüm Satışları silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlConnection baglanti = new SqlConnection(baglantiCumlesi);
                baglanti.Open();
                string komutCumlesi2 = "Delete From Satıs ";
                //string komutCumlesi2 = "Delete From Satıs Where BarkodNO='" + dataGridView1.CurrentRow.Cells["BarkodNO"].Value.ToString() + "'";
                SqlCommand komut2 = new SqlCommand(komutCumlesi2, baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                dataGridView1.DataSource = null;
                MessageBox.Show("Ürün Satısı silindi ");
                SatısListele();
            }
        }
    }
}
