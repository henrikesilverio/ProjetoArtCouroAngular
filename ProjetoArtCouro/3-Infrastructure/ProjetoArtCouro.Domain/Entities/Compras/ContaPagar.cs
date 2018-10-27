using System;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Compras
{
    public class ContaPagar : Notifiable
    {
        public Guid ContaPagarId { get; set; }
        public int ContaPagarCodigo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorDocumento { get; set; }
        public StatusContaPagarEnum StatusContaPagar { get; set; }
        public bool Pago { get; set; }
        public virtual Compra Compra { get; set; }

        public void Validar()
        {
            new ValidationContract<ContaPagar>(this)
                .IsNotEquals(x => x.DataVencimento, new DateTime())
                .IsNotEquals(x => x.StatusContaPagar, StatusVendaEnum.None)
                .IsNotZero(x => x.ValorDocumento)
                .IsNotNull(x => x.Compra, Erros.PurchaseNotSet);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
