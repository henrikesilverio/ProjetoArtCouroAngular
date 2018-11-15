using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pagamentos;

namespace ProjetoArtCouro.DataBase.EntityConfig.PagamentoConfiguration
{
    public class CondicaoPagamentoConfiguration : EntityTypeConfiguration<CondicaoPagamento>
    {
        public CondicaoPagamentoConfiguration()
        {
            ToTable("CondicaoPagamento");

            Property(x => x.CondicaoPagamentoId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CondicaoPagamentoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Descricao)
               .IsRequired()
               .HasMaxLength(30);

            Property(x => x.Ativo)
                .IsRequired();

            Property(x => x.QuantidadeParcelas)
                .IsRequired();
        }
    }
}
