﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.EntityConfig.PessoaConfiguration
{
    public class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration()
        {
            ToTable("Estado");

            Property(x => x.EstadoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.EstadoCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(state => state.EstadoNome)
                .IsRequired()
                .HasMaxLength(250);

            Ignore(x => x.Notifications);

            //Relacionamento 1 : N
            HasMany(x => x.Endereco)
                .WithRequired(x => x.Estado);
        }
    }
}
