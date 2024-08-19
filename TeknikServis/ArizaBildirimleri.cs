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
    public partial class ArizaBildirimleri : Form
    {
        private DataTable personelDataTable;
        private BindingSource bindingSource;

        public ArizaBildirimleri()
        {
            InitializeComponent();
            ArizaBildirimleriGetir();
            txtAra.TextChanged += txtAra_TextChanged;
            this.StartPosition = FormStartPosition.Manual;

            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );



        }
     
        private void ArizaBildirimleri_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Filtre();
            SatirRenk();
            this.Text = "ARIZA BİLDİRİMLERİ";

        }
        private void SatirRenk()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index % 2 == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
            }
        }

        private void ArizaBildirimleriGetir()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ArizaBildirimi";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["Email"].HeaderText = "E-Mail";
                    dataGridView1.Columns["ArizaDetayi"].HeaderText = "Arıza Detayı";
                    dataGridView1.Columns["ResimYolu"].HeaderText = "Resim Yolu";
                    dataGridView1.Columns["MusteriNumarasi"].HeaderText = "Müşteri Numarası";


                }
            }
            dataGridView1.Columns[0].Visible = false;
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtAra.Text;
            bindingSource.Filter = $"Ad LIKE '%{filterText}%' OR Soyad LIKE '%{filterText}%'";
        }
        private void Filtre()
        {
            personelDataTable = new DataTable();
            bindingSource = new BindingSource();

            using (SqlConnection connection = new SqlConnection("Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True"))
            {
                string query = "SELECT MusteriNumarasi,MusteriID, Ad, Soyad, Telefon, Email, ArizaDetayi, ResimYolu FROM ArizaBildirimi";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(personelDataTable);
            }

            bindingSource.DataSource = personelDataTable;
            dataGridView1.DataSource = bindingSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
