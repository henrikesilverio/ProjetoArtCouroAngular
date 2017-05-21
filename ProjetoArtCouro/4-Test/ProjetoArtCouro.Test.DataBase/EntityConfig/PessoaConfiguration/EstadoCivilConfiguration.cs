using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.Test.DataBase.EntityConfig.PessoaConfiguration
{
    public class EstadoCivilConfiguration : EntityTypeConfiguration<EstadoCivil>
    {
        public EstadoCivilConfiguration()
        {
            ToTable("EstadoCivil");

            Property(x => x.EstadoCivilId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.EstadoCivilCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.EstadoCivilNome)
                .IsRequired()
                .HasMaxLength(250);

            Ignore(x => x.Notifications);

            //Relacionamento 1 : N
            HasMany(x => x.PessoaFisica)
                .WithRequired(x => x.EstadoCivil);
        }
    }
}
