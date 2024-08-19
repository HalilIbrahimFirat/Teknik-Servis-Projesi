using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis
{
    class Müsteri
    {
        public int MusteriNumarasi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public double Telefon { get; set; }
        public string Email { get; set; }

        public void ArizaBildir(Ariza ariza, List<string> resimYollari)
        {                                       
        }

        public void Guncelle()
        {
            string connectionString = "Data Source=HALIL;Initial Catalog=TeknikServis;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE ArizaBildirimi SET Ad = @Ad, Soyad = @Soyad, Email=@Email, Telefon = @Telefon WHERE MusteriNumarasi = @MusteriNumarasi";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MusteriNumarasi", MusteriNumarasi);
                    command.Parameters.AddWithValue("@Ad", Ad);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Soyad", Soyad);
                    command.Parameters.AddWithValue("@Telefon", Telefon);

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
