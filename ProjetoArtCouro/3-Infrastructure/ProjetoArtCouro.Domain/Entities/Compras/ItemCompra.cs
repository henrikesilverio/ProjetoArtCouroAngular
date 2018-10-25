using System;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Compras
{
    public class ItemCompra : Notifiable
    {
        public Guid ItemCompraId { get; set; }
        public int ItemCompraCodigo { get; set; }
        public int ProdutoCodigo { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
        public virtual Compra Compra { get; set; }

        public void Validar()
        {
            new ValidationContract<ItemCompra>(this)
                .IsNotZero(x => x.ProdutoCodigo)
                .IsRequired(x => x.ProdutoNome)
                .HasMaxLenght(x => x.ProdutoNome, 200)
                .IsNotZero(x => x.Quantidade)
                .IsNotZero(x => x.PrecoVenda)
                .IsNotZero(x => x.ValorBruto)
                .IsNotZero(x => x.ValorLiquido);

            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
