using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.Test.DataBase.EntityConfig.PessoaConfiguration
{
    public class PessoaFisicaConfiguration : EntityTypeConfiguration<PessoaFisica>
    {
        public PessoaFisicaConfiguration()
        {
            ToTable("PessoaFisica");

            HasKey(x => x.PessoaId);

            Property(x => x.PessoaFisicaCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CPF)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_CPF", 1) { IsUnique = true }));

            Property(x => x.RG)
                .IsRequired()
                .HasMaxLength(15);

            Property(x => x.Sexo)
                .IsRequired()
                .HasMaxLength(10);

            Ignore(x => x.Notifications);
        }
    }
}
