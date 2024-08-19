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
    public partial class Raporla : Form
    {
        public Raporla()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.StartPosition = FormStartPosition.Manual;

            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void btnRaporOlustur_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMusteriNumarasi.Text) || string.IsNullOrWhiteSpace(txtRapor.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Rapor rapor = new Rapor
            {
                MusteriNumarasi = int.Parse(txtMusteriNumarasi.Text),
                RaporDetayi = txtRapor.Text

            };

            rapor.RaporOlustur();



        }

        private void Raporla_Load(object sender, EventArgs e)
        {
            VerileriYukle();
            this.Text = "RAPOR SAYFASI";
        }
        private void VerileriYukle()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            string query = "SELECT ArizaDetayi, MusteriNumarasi FROM ArizaBildirimi";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["MusteriNumarasi"].HeaderText = "Müşteri Numaraları";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void txtMusteriNumarasi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtRapor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
