using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.EntityConfig.PessoaConfiguration
{
    public class MeioComunicacaoConfiguration : EntityTypeConfiguration<MeioComunicacao>
    {
        public MeioComunicacaoConfiguration()
        {
            ToTable("MeioComunicacao");

            Property(x => x.MeioComunicacaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.MeioComunicacaoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.MeioComunicacaoNome)
                .IsRequired()
                .HasMaxLength(250);

            Property(x => x.TipoComunicacao)
                .IsRequired();

            Property(x => x.Principal)
                .IsRequired();

            Ignore(x => x.Notifications);
        }
    }
}
