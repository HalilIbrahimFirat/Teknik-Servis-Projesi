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
    public partial class Kayıt : Form
    {
        string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

        public Kayıt()
        {
            InitializeComponent();
            txtArizaKaydi.Text = Giris.KullaniciAdi;
            dtpPlanlananSaat.Format = DateTimePickerFormat.Time;
            dtpPlanlananSaat.ShowUpDown = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            this.StartPosition = FormStartPosition.Manual;

            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );

            // ComboBox için arama özelliğini aktif et
            cmbMusteriKod.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbMusteriKod.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUrun.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUrun.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                int musteriNumarasi = int.Parse(cmbMusteriKod.SelectedItem.ToString());

                // Aynı müşteri numarası ile daha önce bir kayıt olup olmadığını kontrol et
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Ariza WHERE MusteriNumarasi = @MusteriNumarasi";
                    SqlCommand command = new SqlCommand(checkQuery, connection);
                    command.Parameters.AddWithValue("@MusteriNumarasi", musteriNumarasi);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        DialogResult result = MessageBox.Show("Bu müşteri numarası ile zaten bir kayıt var, devam etmek istiyor musunuz?",
                                                              "Uyarı",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            // Kullanıcı 'Hayır' seçeneğini seçerse işlemi durdur
                            return;
                        }
                    }
                }

                Ariza ariza = new Ariza
                {
                    MusteriNumarasi = musteriNumarasi,
                    UrunBilgisi = cmbUrun.SelectedItem.ToString(),
                    ArizaNedir = txtAriza.Text,
                    ArizaDetayi = txtArizaDetayi.Text,
                    PlanlananTarih = dtpPlanlananTarih.Value,
                    PlanlananSaat = dtpPlanlananSaat.Value,
                    kayitAlan = txtArizaKaydi.Text
                };

                ariza.ArizaKaydiGir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kayıt_Load(object sender, EventArgs e)
        {
            VerileriYukle();
            Urunler();
            this.Text = "ARIZA KAYIT";
        }
        private void Urunler()
        {
            // Veritabanı bağlantı dizesi

            string query = "SELECT UrunAdi FROM Urunler";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // ComboBox'a UrunAdi ekle
                        cmbUrun.Items.Add(reader["UrunAdi"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veriler alınırken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void VerileriYukle()
        {
            string query = "SELECT MusteriNumarasi FROM ArizaBildirimi";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);

                    // Müşteri numaralarını ComboBox'a ekle
                    cmbMusteriKod.Items.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        cmbMusteriKod.Items.Add(row["MusteriNumarasi"].ToString());
                    }

                    // DataGridView'de gösterim
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Columns["MusteriNumarasi"].HeaderText = "Müşteri Numaraları";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
