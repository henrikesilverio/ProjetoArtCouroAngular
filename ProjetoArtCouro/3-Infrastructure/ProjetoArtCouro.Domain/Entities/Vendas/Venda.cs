using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Vendas
{
    public class Venda : Notifiable
    {
        public Guid VendaId { get; set; }
        public int VendaCodigo { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusVendaEnum StatusVenda { get; set; }
        public decimal ValorTotalBruto { get; set; }
        public decimal ValorTotalDesconto { get; set; }
        public decimal ValorTotalLiquido { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Pessoa Cliente { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual CondicaoPagamento CondicaoPagamento { get; set; }
        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
        public virtual ICollection<ContaReceber> ContasReceber { get; set; }

        public void Validar()
        {
            new ValidationContract<Venda>(this)
                .IsNotEquals(x => x.DataCadastro, new DateTime())
                .IsNotEquals(x => x.StatusVenda, StatusVendaEnum.None)
                .IsNotZero(x => x.ValorTotalBruto)
                .IsNotZero(x => x.ValorTotalLiquido)
                .IsNotNull(x => x.ItensVenda, Erros.SaleItemsNotSet);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
