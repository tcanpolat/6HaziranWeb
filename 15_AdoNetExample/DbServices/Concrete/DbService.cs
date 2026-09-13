using _15_AdoNetExample.DbServices.Abstract;
using _15_AdoNetExample.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace _15_AdoNetExample.DbServices.Concrete
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;
        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        // ExecuteNonQuery Ado.Net'te Insert,Update ve Delete işlemlerini
        // gerçekleştirmek için kullanılan bir yöntemdir.
        // Bu yöntem, SQL sorgusunu çalıştırır ve
        // etkilenen satır sayısını döndürür.
        // ExecuteNonQuery, genellikle veri tabanında
        // değişiklik yapmak için kullanılır ve SELECT sorgularında kullanılmaz.
        public void ExecuteNonQuery(string query)
        {   
            // Veri sızıntısını önlemek için using bloğu (dispose) kullanılır.
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Parametreli ExecuteNonQuery metodu, SQL sorgusuna parametreler ekleyerek insert,update,delete yapar.
        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if(parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // ExecuteReader metodu, SQL sorgusunu çalıştırır ve sonuçları okuyarak bir liste döndürür.
        // Select sorgularında kullanılır ve genellikle veri tabanından veri çekmek için tercih edilir.
        public List<Student> ExecuteReader(string query)
        {
            var students = new List<Student>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Age = Convert.ToInt32(reader["Age"])
                            };

                            students.Add(student); // tablodan gelen her satırı listeye ekliyoruz.
                        }
                    }
                }
            }

            return students;
        }
        // ExecuteScalar Aggregate fonksiyonları kullanmak için kullanılan bir yöntemdir.
        // Count, Sum, Avg gibi fonksiyonlar kullanıldığında tek bir değer döndürür.
        // ExecuteScalar geriye object tipinde bir değer döndürür. Bu nedenle, döndürülen değeri uygun bir veri tipine dönüştürmek gerekebilir.
        public object ExecuteScalar(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    return command.ExecuteScalar(); // tek bir değer döndürür. Aggregate fonksiyonları için kullanılır.
                }
            }
        }
    }
}
