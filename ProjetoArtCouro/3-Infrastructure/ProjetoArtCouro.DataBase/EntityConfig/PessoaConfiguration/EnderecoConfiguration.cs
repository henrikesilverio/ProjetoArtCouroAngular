using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.EntityConfig.PessoaConfiguration
{
    public class EnderecoConfiguration : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfiguration()
        {
            ToTable("Endereco");

            Property(x => x.EnderecoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.EnderecoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CEP)
                .IsRequired()
                .HasMaxLength(9);

            Property(x => x.Logradouro)
                .IsRequired()
                .HasMaxLength(200);

            Property(x => x.Bairro)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(6);

            Property(x => x.Complemento)
                .HasMaxLength(50);

            Property(x => x.Cidade)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.Principal)
                .IsRequired();

            Ignore(x => x.Notifications);
        }
    }
}
