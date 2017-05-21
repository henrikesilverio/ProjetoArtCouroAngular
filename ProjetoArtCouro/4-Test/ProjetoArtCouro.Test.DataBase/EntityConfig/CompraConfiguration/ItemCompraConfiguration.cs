using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Compras;

namespace ProjetoArtCouro.Test.DataBase.EntityConfig.CompraConfiguration
{
    public class ItemCompraConfiguration : EntityTypeConfiguration<ItemCompra>
    {
        public ItemCompraConfiguration()
        {
            ToTable("ItemCompra");

            Property(x => x.ItemCompraId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ItemCompraCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ProdutoCodigo)
                .IsRequired();

            Property(x => x.ProdutoNome)
                .IsRequired();

            Property(x => x.Quantidade)
                .IsRequired();

            Property(x => x.PrecoVenda)
                .IsRequired();

            Property(x => x.ValorBruto)
                .IsRequired();

            Property(x => x.ValorLiquido)
                .IsRequired();
        }
    }
}
