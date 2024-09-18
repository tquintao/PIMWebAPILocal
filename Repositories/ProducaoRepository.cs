using Microsoft.Extensions.Configuration;
using PIMWebAPILocal.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PIMWebAPILocal.Repositories
{
    public class ProducaoRepository
    {
        private readonly string _connectionString;

        public ProducaoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Producao> GetProducoes()
        {
            var producoes = new List<Producao>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producoes", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producoes.Add(new Producao
                            {
                                ProducaoId = (int)reader["ProducaoId"],
                                ProdutoId = (int)reader["ProdutoId"],
                                DataProducao = (DateTime)reader["DataProducao"],
                                QuantidadeProduzida = (int)reader["QuantidadeProduzida"]
                            });
                        }
                    }
                }
            }

            return producoes;
        }

        public Producao GetProducaoById(int id)
        {
            Producao producao = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producoes WHERE ProducaoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producao = new Producao
                            {
                                ProducaoId = (int)reader["ProducaoId"],
                                ProdutoId = (int)reader["ProdutoId"],
                                DataProducao = (DateTime)reader["DataProducao"],
                                QuantidadeProduzida = (int)reader["QuantidadeProduzida"]
                            };
                        }
                    }
                }
            }

            return producao;
        }

        public void AddProducao(Producao producao)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producoes (ProdutoId, DataProducao, QuantidadeProduzida) VALUES (@ProdutoId, @DataProducao, @QuantidadeProduzida)", conn))
                {
                    cmd.Parameters.AddWithValue("@ProdutoId", producao.ProdutoId);
                    cmd.Parameters.AddWithValue("@DataProducao", producao.DataProducao);
                    cmd.Parameters.AddWithValue("@QuantidadeProduzida", producao.QuantidadeProduzida);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProducao(Producao producao)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Producoes SET ProdutoId = @ProdutoId, DataProducao = @DataProducao, QuantidadeProduzida = @QuantidadeProduzida WHERE ProducaoId = @ProducaoId", conn))
                {
                    cmd.Parameters.AddWithValue("@ProdutoId", producao.ProdutoId);
                    cmd.Parameters.AddWithValue("@DataProducao", producao.DataProducao);
                    cmd.Parameters.AddWithValue("@QuantidadeProduzida", producao.QuantidadeProduzida);
                    cmd.Parameters.AddWithValue("@ProducaoId", producao.ProducaoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProducao(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Producoes WHERE ProducaoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
