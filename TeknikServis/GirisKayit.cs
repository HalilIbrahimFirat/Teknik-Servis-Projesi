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
    public partial class GirisKayit : Form
    {
        public GirisKayit()
        {
            InitializeComponent();
            SatirRenk();
            SifreGetir();
            this.StartPosition = FormStartPosition.Manual;

            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;
            string sifreTekrar = txtSifreTekrar.Text;
            string kullaniciTipi = rbtnYonetim.Checked ? "Yönetim" : "Personel";

            if (sifre != sifreTekrar)
            {
                MessageBox.Show("Şifreler uyuşmuyor!");
                return;
            }
            if (KullaniciAdiVarMi(kullaniciAdi))
            {
                MessageBox.Show("Bu kullanıcı adı zaten mevcut.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (KullaniciAdi, Sifre, KullaniciTipi) VALUES (@KullaniciAdi, @Sifre, @KullaniciTipi)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", sifre);
                cmd.Parameters.AddWithValue("@KullaniciTipi", kullaniciTipi);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarılı!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanına kaydedilirken bir hata oluştu: " + ex.Message);
                }
            }
            SatirRenk();
            SifreGetir();

        }

        private bool KullaniciAdiVarMi(string kullaniciAdi)
        {
            bool varMi = false;
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE KullaniciAdi = @KullaniciAdi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    varMi = count > 0;
                }
            }

            return varMi;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void GirisKayit_Load(object sender, EventArgs e)
        {
            SifreGetir();
            SatirRenk();
            this.Text = "GİRİŞ KAYDI";


        }

        private void SifreGetir()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);
                    dataGridView1.DataSource = datatable;

                    dataGridView1.Columns["KullaniciAdi"].HeaderText = "Kullanıcı Adı";
                    dataGridView1.Columns["Sifre"].HeaderText = "Şifre";
                    dataGridView1.Columns["KullaniciTipi"].HeaderText = "Kullanıcı Tipi";

                }

            }
            dataGridView1.Columns[0].Visible = false;
        }

        private void SatirRenk()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var kullaniciTipiCell = row.Cells[3]; 

                if (kullaniciTipiCell.Value != null)
                {
                    string kullaniciTipi = kullaniciTipiCell.Value.ToString();

                    
                    if (kullaniciTipi == "Yönetim")
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                   
                    else if (kullaniciTipi == "Personel")
                    {
                        row.DefaultCellStyle.BackColor = Color.BurlyWood;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }
        private void KullaniciSil(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Kullanıcı başarıyla silindi.");
                SifreGetir();
                SatirRenk();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int selectedPersonelID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["Id"].Value);

                KullaniciSil(selectedPersonelID);
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

