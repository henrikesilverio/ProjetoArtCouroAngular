using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class Pessoa
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
            AssertionConcern.AssertArgumentNotEmpty(Nome, Erros.EmptyName);
            AssertionConcern.AssertArgumentNotNull(Papeis, Erros.PaperEmptyPerson);
            AssertionConcern.AssertArgumentNotNull(MeiosComunicacao, Erros.MeansOfCommunicationEmpty);
            AssertionConcern.AssertArgumentNotNull(Enderecos, Erros.EmptyAddress);
        }
    }
}
