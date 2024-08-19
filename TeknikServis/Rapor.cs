using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeknikServis
{
    class Rapor
    {
        public int RaporID { get; set; }
        public string RaporDetayi { get; set; }
        public int MusteriNumarasi { get; set; }

        public void RaporOlustur()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM ArizaBildirimi WHERE MusteriNumarasi = @MusteriNumarasi";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@MusteriNumarasi", MusteriNumarasi);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Müşteri Numarası Bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string query = "INSERT INTO Rapor (RaporDetayi, MusteriNumarasi) VALUES (@RaporDetayi, @MusteriNumarasi)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RaporDetayi", RaporDetayi);
                        command.Parameters.AddWithValue("@MusteriNumarasi", MusteriNumarasi);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Rapor İşlemi Başarıyla Gerçekleşti ", "BAŞARILI", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("HATA: " + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
