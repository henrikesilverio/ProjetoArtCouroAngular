using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Compras;

namespace ProjetoArtCouro.Test.DataBase.EntityConfig.CompraConfiguration
{
    public class ContaPagarConfiguration : EntityTypeConfiguration<ContaPagar>
    {
        public ContaPagarConfiguration()
        {
            ToTable("ContaPagar");

            Property(x => x.ContaPagarId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ContaPagarCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.DataVencimento)
                .IsRequired();

            Property(x => x.ValorDocumento)
                .IsRequired();

            Property(x => x.StatusContaPagar)
                .IsRequired();

            Property(x => x.Pago)
                .IsRequired();
        }
    }
}
