﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProjetoArtCouro.Domain.Entities.Produtos;

namespace ProjetoArtCouro.DataBase.EntityConfig.ProdutoConfiguration
{
    public class UnidadeConfiguration : EntityTypeConfiguration<Unidade>
    {
        public UnidadeConfiguration()
        {
            ToTable("Unidade");

            Property(x => x.UnidadeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.UnidadeCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.UnidadeNome)
               .IsRequired()
               .HasMaxLength(30);

            Ignore(x => x.Notifications);

            //Relacionamento 1 : N
            HasMany(x => x.Produto)
                .WithRequired(x => x.Unidade);
        }
    }
}
