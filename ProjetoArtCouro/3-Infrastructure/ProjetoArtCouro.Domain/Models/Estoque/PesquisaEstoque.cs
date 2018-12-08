namespace ProjetoArtCouro.Domain.Models.Estoque
{
    public class PesquisaEstoque
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string NomeFornecedor { get; set; }
        public int CodigoFornecedor { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
