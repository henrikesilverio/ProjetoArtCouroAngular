using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using System.ComponentModel.DataAnnotations;

namespace ProjetoArtCouro.Domain.Models.Pessoa
{
    public class PesquisaPessoaModel
    {
        [Display(Name = "Code", ResourceType = typeof(Mensagens))]
        public int? Codigo { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Mensagens))]
        public string Nome { get; set; }

        [Display(Name = "CPFCNPJ", ResourceType = typeof(Mensagens))]
        public string CPFCNPJ { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Mensagens))]
        public string Email { get; set; }

        [Display(Name = "TypeOfPerson", ResourceType = typeof(Mensagens))]
        public bool EPessoaFisica { get; set; }

        public TipoPapelPessoaEnum TipoPapelPessoa { get; set; }
    }
}
