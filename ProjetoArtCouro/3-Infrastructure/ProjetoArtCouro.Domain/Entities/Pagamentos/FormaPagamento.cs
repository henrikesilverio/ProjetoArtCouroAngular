using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pagamentos
{
    public class FormaPagamento : Notifiable
    {
        public Guid FormaPagamentoId { get; set; }
        public int FormaPagamentoCodigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Venda> Vendas { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }

        public void Validar()
        {
            new ValidationContract<FormaPagamento>(this)
                .IsRequired(x => x.Descricao)
                .HasMaxLenght(x => x.Descricao, 30)
                .IsRequired(x => x.Ativo);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
