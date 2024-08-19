using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TeknikServis
{
    public partial class IsDurumYonetimi : Form
    {
        private string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

        public IsDurumYonetimi()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            txtAra.TextChanged += txtAra_TextChanged_1;
            this.StartPosition = FormStartPosition.Manual;
            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );


        }

        private void IsDurumYonetimi_Load(object sender, EventArgs e)
        {
            IsDurumYonetimiGetir();
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = "Durum Güncelle";
            btn1.Text = "Bitti";
            btn1.Name = "bitti";
            btn1.UseColumnTextForButtonValue = true;
            btn1.Width = 100;
            dataGridView1.Columns.Add(btn1);
            this.Text = "İŞ DURUM YÖNETİMİ";


            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }



        private void AramaYap(string musteriNumarasi)
        {
            string sorgu = "SELECT TOP 1000 [ArizaID], [MusteriNumarasi], [UrunBilgisi], [ArizaNedir], [ArizaDetayi], [PlanlananTarih], [PlanlananSaat], [kayitAlan], [Durum] FROM [TeknikServis].[dbo].[Ariza] WHERE [MusteriNumarasi] LIKE @MusteriNumarasi";

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@MusteriNumarasi", "%" + musteriNumarasi + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(komut);
                    DataTable veriTablosu = new DataTable();

                    try
                    {
                        baglanti.Open();
                        adapter.Fill(veriTablosu);
                        dataGridView1.DataSource = veriTablosu;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }
                }
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer tıklanan hücre, "Bitti" butonuna aitse
            if (e.ColumnIndex == dataGridView1.Columns["bitti"].Index && e.RowIndex >= 0)
            {
                // Burada sadece seçilen satırın `Durum` değeri güncellenir
                string arizaId = dataGridView1.Rows[e.RowIndex].Cells["ArizaID"].Value.ToString();
                UpdateDurum(arizaId);
            }
        }

        private void UpdateDurum(string arizaId)
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            string updateQuery = "UPDATE [TeknikServis].[dbo].[Ariza] SET [Durum] = 'Bitti' WHERE [ArizaID] = @ArizaID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@ArizaID", arizaId);
                    int rowsAffected = command.ExecuteNonQuery();

                }
            }

            IsDurumYonetimiGetir();
        }

        private void IsDurumYonetimiGetir()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ariza";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            dataGridView1.Columns["ArizaID"].Visible = false;
        }

        private void txtAra_TextChanged_1(object sender, EventArgs e)
        {
            string aramaTerimi = txtAra.Text.Trim();

            if (!string.IsNullOrEmpty(aramaTerimi))
            {
                AramaYap(aramaTerimi);
            }
            else
            {
                dataGridView1.DataSource = null;
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
