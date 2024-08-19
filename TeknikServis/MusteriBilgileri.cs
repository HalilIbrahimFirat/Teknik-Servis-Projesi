using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;



namespace TeknikServis
{
    public partial class MusteriBilgileri : Form
    {

        public MusteriBilgileri()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
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

        private void MusteriBilgileri_Load(object sender, EventArgs e)
        {
            VerileriYukle();
            this.Text = "MÜŞTERİ BİLGİLERİ";
        }
        private void VerileriYukle()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            string query = "SELECT Ad, Soyad, Telefon,Email, MusteriNumarasi FROM ArizaBildirimi";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["Ad"].HeaderText = "Ad";
                    dataGridView1.Columns["Soyad"].HeaderText = "Soyad";
                    dataGridView1.Columns["Telefon"].HeaderText = "Telefon";
                    dataGridView1.Columns["Email"].HeaderText = "E-Mail";
                    dataGridView1.Columns["MusteriNumarasi"].HeaderText = "Müşteri Numarası";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int musteriNumarasi = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["MusteriNumarasi"].Value);

                SilMusteri(musteriNumarasi);
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SilMusteri(int musteriNumarasi)
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ArizaBildirimi WHERE MusteriNumarasi = @MusteriNumarasi";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MusteriNumarasi", musteriNumarasi);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        VerileriYukle();
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi başarısız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int selectedMusteriNumarasi = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["MusteriNumarasi"].Value);

                    string yeniAd = txtAd.Text;
                    string yeniSoyad = txtSoyad.Text;
                    string yeniEmail = txtEmail.Text;
                    double yeniTelNo = Convert.ToDouble(txtTelefon.Text);



                    Müsteri musteri = new Müsteri
                    {
                        MusteriNumarasi = selectedMusteriNumarasi,
                        Ad = yeniAd,
                        Soyad = yeniSoyad,
                        Email = yeniEmail,
                        Telefon = yeniTelNo
                    };

                    musteri.Guncelle();

                    txtAd.Clear();
                    txtSoyad.Clear();
                    txtEmail.Clear();
                    txtTelefon.Clear();

                    VerileriYukle();

                    MessageBox.Show("Müşteri başarıyla güncellendi.");
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
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                txtAd.Text = selectedRow.Cells["Ad"].Value.ToString();
                txtSoyad.Text = selectedRow.Cells["Soyad"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                txtTelefon.Text = selectedRow.Cells["Telefon"].Value.ToString();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; //Lisans ayarlamak için kullanıldı

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
                saveFileDialog.Title = "Excel Dosyası Kaydet";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(saveFileDialog.FileName);
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("MusteriBilgileri");

                        // Başlıkları ekle
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                        }

                        // Verileri ekle
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value;
                            }
                        }

                        excel.SaveAs(fi);
                    }

                    MessageBox.Show("Veriler başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
