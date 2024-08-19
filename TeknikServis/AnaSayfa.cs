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
    public partial class AnaSayfa : Form
    {

        public AnaSayfa()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;

            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );


        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            label3.Text = "HOŞGELDİN "+Giris.KullaniciAdi.ToUpper();
            string kullaniciTipi = GetKullaniciTipi();
            if (kullaniciTipi == "Personel")
            {
                şifreYönetimiToolStripMenuItem.Visible = false;
            }
            this.Text = "ANA SAYFA";
            ArizaBildirimiMenuItemGuncelle();




        }
        private void ArizaBildirimiMenuItemGuncelle()
        {
            if (GenelVeriler.YeniArizaSayisi > 0)
            {
                arızaBildirimleriToolStripMenuItem.Text = $"Arıza Bildirimleri ({GenelVeriler.YeniArizaSayisi}+)";
                arızaBildirimleriToolStripMenuItem.ForeColor = Color.Red; // Yeni arıza varsa rengi kırmızıya değiştir
            }
            else
            {
                arızaBildirimleriToolStripMenuItem.Text = "Arıza Bildirimleri";
                arızaBildirimleriToolStripMenuItem.ForeColor = Color.Black; // Varsayılan renk
            }
        }
        private string GetKullaniciTipi()
        {
            string kullaniciTipi = "";
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            string query = "SELECT KullaniciTipi FROM Users WHERE KullaniciAdi = @KullaniciAdi"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@KullaniciAdi", Giris.KullaniciAdi); 

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        kullaniciTipi = (string)result;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

            return kullaniciTipi;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void bitenVeDevamEdenİşlerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void arızaKyıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kayıt kayit = new Kayıt();
            kayit.Show();
            this.Hide();
        }

        private void arızaRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raporla rapor = new Raporla();
            rapor.Show();
            this.Hide();
        }

        private void personelKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonelKayıt personel = new PersonelKayıt();
            personel.Show();
            this.Hide();
        }

        private void şifreYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GirisKayit giriskayit = new GirisKayit();
            giriskayit.Show();
            this.Hide();
        }

        private void müşteriYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusteriBilgileri musteribilgileri = new MusteriBilgileri();
            musteribilgileri.Show();
            this.Hide();
        }

        private void arızaBildirimleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArizaBildirimleri arizabildirimleri = new ArizaBildirimleri();
            arizabildirimleri.Show();
            this.Hide();
            GenelVeriler.YeniArizaSayisi = 0;
            ArizaBildirimiMenuItemGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();

        }

        private void işDurumYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsDurumYonetimi ısdurumyonetimi = new IsDurumYonetimi();
            ısdurumyonetimi.Show();
            this.Hide();
        }

        private void ürünYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrunYonetim urunyonetim = new UrunYonetim();
            urunyonetim.Show();
            this.Hide();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
