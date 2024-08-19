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

namespace TeknikServis
{
    public partial class UrunYonetim : Form
    {
        string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

        public UrunYonetim()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;

            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );
        }

        private void UrunYonetim_Load(object sender, EventArgs e)
        {
            LoadUrunler();
            this.Text = "ÜRÜN YÖNETİMİ";
        }
        private void LoadUrunler()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Urunler", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; // DataGridView'e verileri yükle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veriler alınırken bir hata oluştu: " + ex.Message);
                }
            }
        }

       

        private void btnUrunEkle_Click_1(object sender, EventArgs e)
        {
            string urunAdi = txtUrunAd.Text;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Urunler (UrunAdi) VALUES (@UrunAdi)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UrunAdi", urunAdi);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ürün eklendi!");
                    LoadUrunler(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ürün eklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void btnUrunGuncelle_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string urunAdi = txtUrunAd.Text;
                int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value); // Seçili satırın UrunID değerini al


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Urunler SET UrunAdi = @UrunAdi WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@UrunAdi", urunAdi);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Ürün güncellendi!");
                        LoadUrunler(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ürün güncellenirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Güncellemek istediğiniz ürünü DataGridView'den seçiniz.");
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value); // Seçili satırın UrunID değerini al


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM Urunler WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Ürün silindi!");
                        LoadUrunler(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ürün silinirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz ürünü DataGridView'den seçiniz.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}