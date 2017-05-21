using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Usuarios;

namespace ProjetoArtCouro.Test.DataBase.EntityConfig.UsuarioConfiguration
{
    public class GrupoPermissaoConfiguration : EntityTypeConfiguration<GrupoPermissao>
    {
        public GrupoPermissaoConfiguration()
        {
            ToTable("GrupoPermissao");

            Property(x => x.GrupoPermissaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.GrupoPermissaoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.GrupoPermissaoNome)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(x => x.Permissoes);

            //Relacionamento 1 : N
            HasMany(x => x.Usuarios)
                .WithRequired(x => x.GrupoPermissao);
        }
    }
}
