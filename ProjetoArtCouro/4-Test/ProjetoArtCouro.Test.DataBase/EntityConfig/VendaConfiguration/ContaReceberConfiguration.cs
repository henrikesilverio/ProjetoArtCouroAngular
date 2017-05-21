using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Vendas;

namespace ProjetoArtCouro.Test.DataBase.EntityConfig.VendaConfiguration
{
    public class ContaReceberConfiguration : EntityTypeConfiguration<ContaReceber>
    {
        public ContaReceberConfiguration()
        {
            ToTable("ContaReceber");

            Property(x => x.ContaReceberId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ContaReceberCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.DataVencimento)
                .IsRequired();

            Property(x => x.ValorDocumento)
                .IsRequired();

            Property(x => x.StatusContaReceber)
                .IsRequired();

            Property(x => x.Recebido)
                .IsRequired();
        }
    }
}
