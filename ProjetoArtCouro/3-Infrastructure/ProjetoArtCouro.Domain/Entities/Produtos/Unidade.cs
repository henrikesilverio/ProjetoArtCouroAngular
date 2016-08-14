using System;
using System.Collections.Generic;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Domain.Entities.Produtos
{
    public class Unidade
    {
        public Guid UnidadeId { get; set; }
        public int UnidadeCodigo { get; set; }
        public string UnidadeNome { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }

        public void Validar()
        {
            AssertionConcern.AssertArgumentNotEquals(0, UnidadeCodigo, string.Format(Erros.NotZeroParameter, "UnidadeCodigo"));
        }
    }
}
