using System;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class Endereco : Notifiable
    {
        public Guid EnderecoId { get; set; }
        public int EnderecoCodigo { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public bool Principal { get; set; }
        public Guid PessoaId { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public void Validar()
        {
            new ValidationContract<Endereco>(this)
                .IsRequired(x => x.CEP)
                .HasMaxLenght(x => x.CEP, 9)
                .IsRequired(x => x.Logradouro)
                .HasMaxLenght(x => x.Logradouro, 200)
                .IsRequired(x => x.Bairro)
                .HasMaxLenght(x => x.Bairro, 50)
                .IsRequired(x => x.Numero)
                .HasMaxLenght(x => x.Numero, 6)
                .HasMaxLenght(x => x.Complemento, 50)
                .IsRequired(x => x.Cidade)
                .HasMaxLenght(x => x.Cidade, 50)
                .IsRequired(x => x.Principal)
                .IsNotNull(x => x.Estado, Erros.EmptyState);
            if (!IsValid())
            {
                throw new InvalidOperationException(GetMergeNotifications());
            }
        }
    }
}
