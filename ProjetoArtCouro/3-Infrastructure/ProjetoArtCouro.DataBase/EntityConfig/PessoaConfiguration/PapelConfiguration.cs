﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.EntityConfig.PessoaConfiguration
{
    public class PapelConfiguration : EntityTypeConfiguration<Papel>
    {
        public PapelConfiguration()
        {
            ToTable("Papel");

            Property(x => x.PapelId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.PapelCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.PapelNome)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnType("varchar");

            Ignore(x => x.Notifications);

            HasMany(x => x.Pessoas);
        }
    }
}
