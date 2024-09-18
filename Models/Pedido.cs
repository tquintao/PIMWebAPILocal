namespace PIMWebAPILocal.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal TotalPedido { get; set; }
    }
}
