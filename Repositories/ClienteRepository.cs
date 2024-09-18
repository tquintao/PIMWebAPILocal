using Microsoft.Extensions.Configuration;
using PIMWebAPILocal.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PIMWebAPILocal.Repositories
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                ClienteId = (int)reader["ID_Cliente"],
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telefone = reader["Telefone"].ToString()
                            });
                        }
                    }
                }
            }

            return clientes;
        }

        public Cliente GetClienteById(int id)
        {
            Cliente cliente = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE ClienteId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                ClienteId = (int)reader["ClienteId"],
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telefone = reader["Telefone"].ToString()
                            };
                        }
                    }
                }
            }

            return cliente;
        }

        public void AddCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Clientes (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)", conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método UpdateCliente (se necessário)
        public void UpdateCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Clientes SET Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE ClienteId = @ClienteId", conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método DeleteCliente (se necessário)
        public void DeleteCliente(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Cliente WHERE ClienteId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
