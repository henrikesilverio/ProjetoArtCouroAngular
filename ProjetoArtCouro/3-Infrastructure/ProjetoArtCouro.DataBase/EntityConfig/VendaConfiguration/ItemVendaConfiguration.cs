using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Vendas;

namespace ProjetoArtCouro.DataBase.EntityConfig.VendaConfiguration
{
    public class ItemVendaConfiguration : EntityTypeConfiguration<ItemVenda>
    {
        public ItemVendaConfiguration()
        {
            ToTable("ItemVenda");

            Property(x => x.ItemVendaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ItemVendaCodigo)
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

            Property(x => x.ValorDesconto)
                .IsRequired();

            Property(x => x.ValorLiquido)
                .IsRequired();
        }
    }
}
