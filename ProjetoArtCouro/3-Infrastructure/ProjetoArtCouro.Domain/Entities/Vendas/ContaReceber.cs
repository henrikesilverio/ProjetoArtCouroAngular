using System;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Vendas
{
    public class ContaReceber : Notifiable
    {
        public Guid ContaReceberId { get; set; }
        public int ContaReceberCodigo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorDocumento { get; set; }
        public StatusContaReceberEnum StatusContaReceber { get; set; }
        public bool Recebido { get; set; }
        public virtual Venda Venda { get; set; }

        public void Validar()
        {
            new ValidationContract<ContaReceber>(this)
                .IsNotEquals(x => x.DataVencimento, new DateTime())
                .IsNotEquals(x => x.StatusContaReceber, StatusContaReceberEnum.None)
                .IsNotZero(x => x.ValorDocumento)
                .IsNotNull(x => x.Venda, Erros.SaleNotSet);

            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
