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
    public partial class PersonelKayıt : Form
    {
        private DataTable personelDataTable;
        private BindingSource bindingSource;
        public PersonelKayıt()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            PersonelVeriGetir();
            txtAra.TextChanged += txtAra_TextChanged;
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

        private void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string ad = txtAd.Text;
                string soyad = txtSoyad.Text;
                double telNo = double.Parse(txtTelNo.Text);

                if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad) || string.IsNullOrWhiteSpace(txtTelNo.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.");
                    return;
                }

                if (!double.TryParse(txtTelNo.Text, out telNo))
                {
                    MessageBox.Show("Telefon numarası geçerli bir sayı olmalıdır.");
                    return;
                }

                if (TelefonNumarasiVarMi(telNo))
                {
                    MessageBox.Show("Bu Kullanıcı Zaten Mevcut.");
                    return;
                }

                


                Personel personel = new Personel
                {
                    Ad = ad,
                    Soyad = soyad,
                    TelNo = telNo
                };
              
               
                PersonelVeriGetir();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void PersonelVeriGetir()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Personel";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["TelNo"].HeaderText = "Telefon Numarası";


                }
            }
            dataGridView1.Columns[0].Visible = false;
        }


        private void PersonelKayıt_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Filtre();
            SatirRenk();
            this.Text = "PERSONEL KAYIT";

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

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtAra.Text;
            bindingSource.Filter = $"Ad LIKE '%{filterText}%' OR Soyad LIKE '%{filterText}%'";
        }

        private void btnPersonelGuncelle_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text) || string.IsNullOrWhiteSpace(txtTelNo.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.");
                    return;
                }

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int selectedPersonelID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["PersonelID"].Value);

                    string yeniAd = txtAd.Text;
                    string yeniSoyad = txtSoyad.Text;
                    double yeniTelNo = Convert.ToDouble(txtTelNo.Text);



                    Personel personel = new Personel
                    {
                        PersonelID = selectedPersonelID,
                        Ad = yeniAd,
                        Soyad = yeniSoyad,
                        TelNo = yeniTelNo
                    };

                    personel.Guncelle();

                    txtAd.Clear();
                    txtSoyad.Clear();
                    txtTelNo.Clear();

                    PersonelVeriGetir();

                    MessageBox.Show("Personel başarıyla güncellendi.");
                }
                else
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private bool TelefonNumarasiVarMi(double telNo)
        {
            bool varMi = false;
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Personel WHERE TelNo = @TelNo";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TelNo", telNo);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    varMi = count > 0;
                }
            }

            return varMi;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                txtAd.Text = selectedRow.Cells["Ad"].Value.ToString();
                txtSoyad.Text = selectedRow.Cells["Soyad"].Value.ToString();
                txtTelNo.Text = selectedRow.Cells["TelNo"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Filtre()
        {
            personelDataTable = new DataTable();
            bindingSource = new BindingSource();

            using (SqlConnection connection = new SqlConnection("Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True"))
            {
                string query = "SELECT PersonelID, Ad, Soyad, TelNo FROM Personel";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(personelDataTable);
            }

            bindingSource.DataSource = personelDataTable;
            dataGridView1.DataSource = bindingSource;
        }

        private void SilPersonel(int personelID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "DELETE FROM Personel WHERE PersonelID = @PersonelID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PersonelID", personelID);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Personel başarıyla silindi.");
                PersonelVeriGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }



        private void btnSil_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int selectedPersonelID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["PersonelID"].Value);

                SilPersonel(selectedPersonelID);
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
