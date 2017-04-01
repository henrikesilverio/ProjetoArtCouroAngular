using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class Pessoa : Notifiable
    {
        public Guid PessoaId { get; set; }
        public int PessoaCodigo { get; set; }
        public string Nome { get; set; }
        public virtual PessoaFisica PessoaFisica { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
        public virtual ICollection<Venda> Vendas { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Papel> Papeis { get; set; }
        public virtual ICollection<MeioComunicacao> MeiosComunicacao { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }

        public void Validar()
        {
            new ValidationContract<Pessoa>(this)
                .IsRequired(x => x.Nome)
                .HasMaxLenght(x => x.Nome, 150)
                .IsNotNull(x => x.Papeis, Erros.PaperEmptyPerson)
                .IsNotNull(x => x.MeiosComunicacao, Erros.MeansOfCommunicationEmpty)
                .IsNotNull(x => x.Enderecos, Erros.EmptyAddress);
            if (!IsValid())
            {
                throw new InvalidOperationException(GetMergeNotifications());
            }
        }
    }
}
