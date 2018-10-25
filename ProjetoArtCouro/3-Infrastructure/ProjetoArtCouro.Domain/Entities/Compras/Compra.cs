using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Compras
{
    public class Compra : Notifiable
    {
        public Guid CompraId { get; set; }
        public int CompraCodigo { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusCompraEnum StatusCompra { get; set; }
        public decimal ValorTotalBruto { get; set; }
        public decimal ValorTotalFrete { get; set; }
        public decimal ValorTotalLiquido { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual CondicaoPagamento CondicaoPagamento { get; set; }
        public virtual ICollection<Estoque> Estoques { get; set; }
        public virtual ICollection<ItemCompra> ItensCompra { get; set; }
        public virtual ICollection<ContaPagar> ContasPagar { get; set; }

        public void Validar()
        {
            new ValidationContract<Compra>(this)
                .IsNotEquals(x => x.DataCadastro, new DateTime())
                .IsNotEquals(x => x.StatusCompra, StatusCompraEnum.None)
                .IsNotZero(x => x.ValorTotalBruto)
                .IsNotZero(x => x.ValorTotalLiquido)
                .IsNotNull(x => x.ItensCompra, Erros.PurchaseItemsNotInformed);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
