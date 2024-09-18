using Microsoft.Extensions.Configuration;
using PIMWebAPILocal.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;  // Certifique-se de incluir esse namespace para DateTime e Exception

namespace PIMWebAPILocal.Repositories
{
    public class PedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Método para buscar todos os pedidos
        public List<Pedido> GetPedidos()
        {
            var pedidos = new List<Pedido>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pedidos", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedidos.Add(new Pedido
                            {
                                PedidoId = (int)reader["PedidoId"],
                                ClienteId = (int)reader["ClienteId"],
                                DataPedido = (DateTime)reader["DataPedido"],
                                TotalPedido = (decimal)reader["TotalPedido"]
                            });
                        }
                    }
                }
            }

            return pedidos;
        }

        // Método para buscar um pedido pelo ID
        public Pedido GetPedidoById(int id)
        {
            Pedido pedido = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pedidos WHERE PedidoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pedido = new Pedido
                            {
                                PedidoId = (int)reader["PedidoId"],
                                ClienteId = (int)reader["ClienteId"],
                                DataPedido = (DateTime)reader["DataPedido"],
                                TotalPedido = (decimal)reader["TotalPedido"]
                            };
                        }
                    }
                }
            }

            return pedido;
        }

        // Método para adicionar um pedido
        public void AddPedido(Pedido pedido)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Pedidos (ClienteId, DataPedido, TotalPedido) VALUES (@ClienteId, @DataPedido, @TotalPedido)", conn))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", pedido.ClienteId);
                    cmd.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                    cmd.Parameters.AddWithValue("@TotalPedido", pedido.TotalPedido);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para atualizar um pedido
        public void UpdatePedido(Pedido pedido)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Pedidos SET ClienteId = @ClienteId, DataPedido = @DataPedido, TotalPedido = @TotalPedido WHERE PedidoId = @PedidoId", conn))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", pedido.ClienteId);
                    cmd.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                    cmd.Parameters.AddWithValue("@TotalPedido", pedido.TotalPedido);
                    cmd.Parameters.AddWithValue("@PedidoId", pedido.PedidoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para deletar um pedido
        public void DeletePedido(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Pedidos WHERE PedidoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
