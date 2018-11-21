using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.Domain.Models.Pessoa
{
    public class PesquisaPessoaJuridica
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public TipoPapelPessoaEnum TipoPapelPessoa { get; set; }
    }
}
