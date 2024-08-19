using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TeknikServis
{
    public partial class Giris : Form
    {
        private string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

        public static string KullaniciAdi { get; set; }
        public Giris()
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

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            KullaniciAdi = txtKullaniciAdi.Text;

            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;
            string kullaniciTipi = rbtnYonetim.Checked ? "Yönetim" : "Personel";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE KullaniciAdi=@KullaniciAdi AND Sifre=@Sifre AND KullaniciTipi=@KullaniciTipi";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@Sifre", sifre);
                cmd.Parameters.AddWithValue("@KullaniciTipi", kullaniciTipi);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        MessageBox.Show("Giriş başarılı!");
                        AnaSayfa anasayfa = new AnaSayfa();
                        anasayfa.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı, şifre veya kullanıcı tipi hatalı!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısında bir hata oluştu: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            string query = @"
    SELECT 
        a.MusteriNumarasi, 
        a.PlanlananTarih, 
        a.PlanlananSaat, 
        a.kayitAlan, 
        a.Durum, 
        r.RaporDetayi 
    FROM 
        Ariza a
    LEFT JOIN 
        Rapor r ON a.MusteriNumarasi = r.MusteriNumarasi";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    // Sütun başlıklarını ayarlama
                    dataGridView1.Columns["MusteriNumarasi"].HeaderText = "Müşteri Numarası";
                    dataGridView1.Columns["PlanlananTarih"].HeaderText = "Planlanan Tarih";
                    dataGridView1.Columns["PlanlananSaat"].HeaderText = "Planlanan Saat";
                    dataGridView1.Columns["kayitAlan"].HeaderText = "Kayıt Alan Personel";
                    dataGridView1.Columns["Durum"].HeaderText = "Durum";
                    dataGridView1.Columns["RaporDetayi"].HeaderText = "Rapor Detayı";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

            this.Text = "GİRİŞ EKRANI";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
        private void txtAra_TextChanged(object sender, EventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
