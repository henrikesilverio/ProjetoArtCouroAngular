using System;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class PessoaFisica : Notifiable
    {
        public Guid PessoaId { get; set; }
        public int PessoaFisicaCodigo { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Sexo { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }

        public void Validar()
        {
            new ValidationContract<PessoaFisica>(this)
                .IsRequired(x => x.CPF)
                .IsRequired(x => x.RG)
                .HasMaxLenght(x => x.RG, 15)
                .IsRequired(x => x.Sexo)
                .HasMaxLenght(x => x.Sexo, 10)
                .IsNotNull(x => x.Pessoa, Erros.EmptyPerson)
                .IsNotNull(x => x.EstadoCivil, Erros.EmptyMaritalStatus);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
