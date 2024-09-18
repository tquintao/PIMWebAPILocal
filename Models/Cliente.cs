namespace PIMWebAPILocal.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Adicione a propriedade Telefone se ela for necessária
        public string Telefone { get; set; }
    }
}
