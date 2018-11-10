using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class Estado : Notifiable
    {
        public Guid EstadoId { get; set; }
        public int EstadoCodigo { get; set; }
        public string EstadoNome { get; set; }
        public virtual ICollection<Endereco> Endereco { get; set; }

        public void Validar()
        {
            new ValidationContract<Estado>(this)
                .IsRequired(x => x.EstadoNome)
                .HasMaxLenght(x => x.EstadoNome, 250);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
