using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.Domain.Models.Pessoa
{
    public class PesquisaPessoaFisica
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public TipoPapelPessoaEnum TipoPapelPessoa { get; set; }
    }
}
