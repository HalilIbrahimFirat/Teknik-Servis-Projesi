using System;
using System.Data.SqlClient;

namespace TeknikServis
{
    class Personel
    {

        public string Ad { get; set; }
        public string Soyad { get; set; }
        public double TelNo { get; set; }
        public int PersonelID { get; set; }

        public void PersonelEkle()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Personel (Ad, Soyad, TelNo) VALUES (@Ad, @Soyad, @TelNo)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ad", Ad);
                    command.Parameters.AddWithValue("@Soyad", Soyad);
                    command.Parameters.AddWithValue("@TelNo", TelNo);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void Guncelle()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Personel SET Ad = @Ad, Soyad = @Soyad, TelNo = @TelNo WHERE PersonelID = @PersonelID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonelID", PersonelID);
                    command.Parameters.AddWithValue("@Ad", Ad);
                    command.Parameters.AddWithValue("@Soyad", Soyad);
                    command.Parameters.AddWithValue("@TelNo", TelNo);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
