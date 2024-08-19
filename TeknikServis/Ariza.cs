using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeknikServis
{
    class Ariza : Müsteri
    {
        public int ArizaID { get; set; }        public string UrunBilgisi { get; set; }
        public string ArizaNedir { get; set; }
        public string ArizaDetayi { get; set; }
        public DateTime PlanlananTarih { get; set; }
        public DateTime PlanlananSaat { get; set; }

        public string kayitAlan { get; set; }
        public List<string> ResimYollari { get; set; } = new List<string>();

        public void ArizaKaydiGir()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM ArizaBildirimi WHERE MusteriNumarasi = @MusteriNumarasi";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MusteriNumarasi", MusteriNumarasi);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Müşteri Numarası Bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Ariza (MusteriNumarasi, UrunBilgisi, ArizaNedir, ArizaDetayi, PlanlananTarih, PlanlananSaat, kayitAlan) " +
                                         "VALUES (@MusteriNumarasi, @UrunBilgisi, @ArizaNedir, @ArizaDetayi, @PlanlananTarih, @PlanlananSaat, @kayitAlan)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@MusteriNumarasi", MusteriNumarasi);
                        insertCmd.Parameters.AddWithValue("@UrunBilgisi", UrunBilgisi);
                        insertCmd.Parameters.AddWithValue("@ArizaNedir", ArizaNedir);
                        insertCmd.Parameters.AddWithValue("@ArizaDetayi", ArizaDetayi);
                        insertCmd.Parameters.AddWithValue("@PlanlananTarih", PlanlananTarih);
                        insertCmd.Parameters.AddWithValue("@PlanlananSaat", PlanlananSaat.TimeOfDay);
                        insertCmd.Parameters.AddWithValue("@kayitAlan", kayitAlan);

                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt Başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Kayıt Eklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void ArizaDetaylariniGor()
        {
        }

        public void ArizaPlanla()
        {
        }

        public void DurumGuncelle()
        {
        }
    }
}
