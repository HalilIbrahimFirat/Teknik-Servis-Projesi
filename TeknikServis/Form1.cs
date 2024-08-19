using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknikServis
{
    public partial class Form1 : Form
    {
        private string resimYolu;


        public Form1()
        {
            InitializeComponent();
           
            txtAd.Enter += TextBox_Enter;
            txtAd.Leave += TextBox_Leave;

            txtSoyad.Enter += TextBox_Enter;
            txtSoyad.Leave += TextBox_Leave;

            txtTelefon.Enter += TextBox_Enter;
            txtTelefon.Leave += TextBox_Leave;

            txtEmail.Enter += TextBox_Enter;
            txtEmail.Leave += TextBox_Leave;

            txtArizaDetayi.Enter += TextBox_Enter;
            txtArizaDetayi.Leave += TextBox_Leave;
            this.StartPosition = FormStartPosition.Manual;


            // Ekran merkezini hesapla ve formun lokasyonunu ayarla
            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
            );
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();

            giris.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa anasayfa = new AnaSayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        

        private void btnGonder_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            string telefon = txtTelefon.Text;
            string email = txtEmail.Text;
            string arizaDetayi = txtArizaDetayi.Text;

            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad) ||
                string.IsNullOrWhiteSpace(telefon) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(arizaDetayi))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            if (telefon.Length != 11 || !telefon.All(char.IsDigit))
            {
                MessageBox.Show("Telefon numarası 11 haneli olmalı ve sadece rakamlardan oluşmalıdır.");
                return;
            }

            if (!GecerliEpostaMi(email))
            {
                MessageBox.Show("Geçerli bir e-posta adresi giriniz.");
                return;
            }

            if (arizaDetayi.Split(' ').Length > 200)
            {
                MessageBox.Show("Arıza detayı en fazla 200 kelime olmalıdır.");
                return;
            }


            string resimYoluToUse = resimYolu ?? DBNull.Value.ToString();

            Random random = new Random();
            int musteriNumarasi = random.Next(10000, 99999);
            label7.Text = musteriNumarasi.ToString();

            using (SqlConnection connection = new SqlConnection("Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True"))
            {
                connection.Open();
                string query = "INSERT INTO ArizaBildirimi (MusteriNumarasi,Ad, Soyad, Telefon, Email, ArizaDetayi, ResimYolu) VALUES (@MusteriNumarasi,@Ad, @Soyad, @Telefon, @Email, @ArizaDetayi, @ResimYolu)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MusteriNumarasi", musteriNumarasi);
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@ArizaDetayi", arizaDetayi);
                    command.Parameters.AddWithValue("@ResimYolu", resimYoluToUse);
                    GenelVeriler.YeniArizaSayisi++;

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Arıza bildirimi başarıyla kaydedildi. Müşteri kodunuzu lütfen not alınız.");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private bool GecerliEpostaMi(string eposta)
        {
            try
            {
                var adres = new System.Net.Mail.MailAddress(eposta);
                return adres.Address == eposta;
            }
            catch
            {
                return false;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;  //TextBox Kontrolü 
            if (txt != null)
            {
                txt.BackColor = Color.LightSteelBlue; 
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;  //TextBox Kontrolü 
            if (txt != null)
            {
                txt.BackColor = Color.White; 
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
          this.Text = "GİRİŞ EKRANI";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    resimYolu = Path.Combine(Application.StartupPath, "YuklenenResimler", Path.GetFileName(ofd.FileName));
                    Directory.CreateDirectory(Path.GetDirectoryName(resimYolu));
                    File.Copy(ofd.FileName, resimYolu, true);
                    MessageBox.Show("Resim başarıyla yüklendi: " + resimYolu);
                }
            }


        }
    }
}
