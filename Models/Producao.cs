namespace PIMWebAPILocal.Models
{
    public class Producao
    {
        public int ProducaoId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public DateTime DataProducao { get; set; }
        public int QuantidadeProduzida { get; set; }
    }
}
