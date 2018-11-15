﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Usuarios;

namespace ProjetoArtCouro.DataBase.EntityConfig.UsuarioConfiguration
{
    public class PermissaoConfiguration : EntityTypeConfiguration<Permissao>
    {
        public PermissaoConfiguration()
        {
            ToTable("Permissao");

            Property(x => x.PermissaoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.PermissaoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.PermissaoNome)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.AcaoNome)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(x => x.GrupoPermissao)
                .WithMany(x => x.Permissoes)
                .Map(m =>
                {
                    m.MapLeftKey("PermissaoId");
                    m.MapRightKey("GrupoPermissaoId");
                    m.ToTable("PermissaoGrupoPermissao");
                });

            HasMany(x => x.Usuarios);
        }
    }
}
