using System;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Vendas
{
    public class ItemVenda : Notifiable
    {
        public Guid ItemVendaId { get; set; }
        public int ItemVendaCodigo { get; set; }
        public int ProdutoCodigo { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorLiquido { get; set; }
        public virtual Venda Venda { get; set; }

        public void Validar()
        {
            new ValidationContract<ItemVenda>(this)
                .IsNotZero(x => x.ProdutoCodigo)
                .IsRequired(x => x.ProdutoNome)
                .HasMaxLenght(x => x.ProdutoNome, 200)
                .IsNotZero(x => x.Quantidade)
                .IsNotZero(x => x.PrecoVenda)
                .IsNotZero(x => x.ValorBruto)
                .IsNotZero(x => x.ValorDesconto)
                .IsNotZero(x => x.ValorLiquido);

            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
