using Microsoft.Extensions.Configuration;
using PIMWebAPILocal.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PIMWebAPILocal.Repositories
{
    public class ProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Produto> GetProdutos()
        {
            var produtos = new List<Produto>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Produtos", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produtos.Add(new Produto
                            {
                                ProdutoId = (int)reader["ProdutoId"],
                                Nome = reader["Nome"].ToString(),
                                Categoria = reader["Categoria"].ToString(),
                                Preco = (decimal)reader["Preco"]
                            });
                        }
                    }
                }
            }

            return produtos;
        }

        public Produto GetProdutoById(int id)
        {
            Produto produto = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Produtos WHERE ProdutoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            produto = new Produto
                            {
                                ProdutoId = (int)reader["ProdutoId"],
                                Nome = reader["Nome"].ToString(),
                                Categoria = reader["Categoria"].ToString(),
                                Preco = (decimal)reader["Preco"]
                            };
                        }
                    }
                }
            }

            return produto;
        }

        public void AddProduto(Produto produto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Produtos (Nome, Categoria, Preco) VALUES (@Nome, @Categoria, @Preco)", conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduto(Produto produto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Produtos SET Nome = @Nome, Categoria = @Categoria, Preco = @Preco WHERE ProdutoId = @ProdutoId", conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@ProdutoId", produto.ProdutoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduto(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Produtos WHERE ProdutoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
