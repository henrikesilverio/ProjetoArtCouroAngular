using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pagamentos
{
    public class CondicaoPagamento : Notifiable
    {
        public Guid CondicaoPagamentoId { get; set; }
        public int CondicaoPagamentoCodigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int QuantidadeParcelas { get; set; }
        public virtual ICollection<Venda> Vendas { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }

        public void Validar()
        {
            new ValidationContract<CondicaoPagamento>(this)
                .IsRequired(x => x.Descricao)
                .HasMaxLenght(x => x.Descricao, 30)
                .IsNotZero(x => x.QuantidadeParcelas);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
