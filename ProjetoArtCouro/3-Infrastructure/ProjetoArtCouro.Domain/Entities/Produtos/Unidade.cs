using System;
using System.Collections.Generic;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Produtos
{
    public class Unidade : Notifiable
    {
        public Guid UnidadeId { get; set; }
        public int UnidadeCodigo { get; set; }
        public string UnidadeNome { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }

        public void Validar()
        {
            new ValidationContract<Unidade>(this)
                .IsNotZero(x => x.UnidadeCodigo)
                .IsRequired(x => x.UnidadeNome)
                .HasMaxLenght(x => x.UnidadeNome, 30);
            if (!IsValid())
            {
                throw new InvalidOperationException(GetMergeNotifications());
            }
        }
    }
}
