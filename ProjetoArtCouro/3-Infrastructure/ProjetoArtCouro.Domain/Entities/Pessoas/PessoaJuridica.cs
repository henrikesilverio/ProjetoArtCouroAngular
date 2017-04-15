using System;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class PessoaJuridica : Notifiable
    {
        public Guid PessoaId { get; set; }
        public int PessoaJuridicaCodigo { get; set; }
        public string CNPJ { get; set; }
        public string Contato { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public void Validar()
        {
            new ValidationContract<PessoaJuridica>(this)
                .IsRequired(x => x.CNPJ)
                .HasMaxLenght(x => x.Contato, 100)
                .IsNotNull(x => x.Pessoa, Erros.EmptyPerson);
            if (!IsValid())
            {
                throw new InvalidOperationException(GetMergeNotifications());
            }
        }
    }
}
