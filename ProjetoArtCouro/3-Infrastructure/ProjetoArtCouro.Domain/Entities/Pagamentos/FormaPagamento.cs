using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Domain.Entities.Pagamentos
{
    public class FormaPagamento
    {
        public Guid FormaPagamentoId { get; set; }
        public int FormaPagamentoCodigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Venda> Vendas { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }

        public void Validar()
        {
            //AssertionConcern.AssertArgumentNotNull(Descricao, string.Format(Erros.NullParameter, "Descricao"));
            //AssertionConcern.AssertArgumentNotEmpty(Descricao, string.Format(Erros.EmptyParameter, "Descricao"));
        }
    }
}
