﻿using System;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Produtos
{
    public class Produto : Notifiable
    {
        public Guid ProdutoId { get; set; }
        public int ProdutoCodigo { get; set; }
        public string ProdutoNome { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public virtual Unidade Unidade { get; set; }
        public virtual Estoque Estoque { get; set; }

        public void Validar()
        {
            new ValidationContract<Produto>(this)
                .IsRequired(x => x.ProdutoNome)
                .HasMaxLenght(x => x.ProdutoNome, 200)
                .IsNotZero(x => x.PrecoVenda)
                .IsNotZero(x => x.PrecoCusto)
                .IsNotNull(x => x.Unidade);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
