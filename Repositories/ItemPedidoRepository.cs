using Microsoft.Extensions.Configuration;
using PIMWebAPILocal.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PIMWebAPILocal.Repositories
{
    public class ItemPedidoRepository
    {
        private readonly string _connectionString;

        public ItemPedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ItemPedido> GetItemPedidos()
        {
            var itens = new List<ItemPedido>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ItemPedidos", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itens.Add(new ItemPedido
                            {
                                ItemPedidoId = (int)reader["ItemPedidoId"],
                                PedidoId = (int)reader["PedidoId"],
                                ProdutoId = (int)reader["ProdutoId"],
                                Quantidade = (int)reader["Quantidade"],
                                PrecoUnitario = (decimal)reader["PrecoUnitario"]
                            });
                        }
                    }
                }
            }

            return itens;
        }

        public ItemPedido GetItemPedidoById(int id)
        {
            ItemPedido item = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ItemPedidos WHERE ItemPedidoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ItemPedido
                            {
                                ItemPedidoId = (int)reader["ItemPedidoId"],
                                PedidoId = (int)reader["PedidoId"],
                                ProdutoId = (int)reader["ProdutoId"],
                                Quantidade = (int)reader["Quantidade"],
                                PrecoUnitario = (decimal)reader["PrecoUnitario"]
                            };
                        }
                    }
                }
            }

            return item;
        }

        public void AddItemPedido(ItemPedido item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ItemPedidos (PedidoId, ProdutoId, Quantidade, PrecoUnitario) VALUES (@PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario)", conn))
                {
                    cmd.Parameters.AddWithValue("@PedidoId", item.PedidoId);
                    cmd.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                    cmd.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                    cmd.Parameters.AddWithValue("@PrecoUnitario", item.PrecoUnitario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateItemPedido(ItemPedido item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE ItemPedidos SET PedidoId = @PedidoId, ProdutoId = @ProdutoId, Quantidade = @Quantidade, PrecoUnitario = @PrecoUnitario WHERE ItemPedidoId = @ItemPedidoId", conn))
                {
                    cmd.Parameters.AddWithValue("@PedidoId", item.PedidoId);
                    cmd.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                    cmd.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                    cmd.Parameters.AddWithValue("@PrecoUnitario", item.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@ItemPedidoId", item.ItemPedidoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItemPedido(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ItemPedidos WHERE ItemPedidoId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
