using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Produtos;

namespace ProjetoArtCouro.DataBase.EntityConfig.ProdutoConfiguration
{
    public class ProdutoConfiguration : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            ToTable("Produto");

            Property(x => x.ProdutoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ProdutoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ProdutoNome)
               .IsRequired()
               .HasMaxLength(200);

            Property(x => x.PrecoVenda)
                .IsRequired();

            Property(x => x.PrecoCusto)
                .IsRequired();

            Ignore(x => x.Notifications);
        }
    }
}
