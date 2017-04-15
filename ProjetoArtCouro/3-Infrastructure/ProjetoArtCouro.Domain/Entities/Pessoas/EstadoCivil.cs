using System;
using System.Collections.Generic;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class EstadoCivil : Notifiable
    {
        public Guid EstadoCivilId { get; set; }
        public int EstadoCivilCodigo { get; set; }
        public string EstadoCivilNome { get; set; }
        public virtual ICollection<PessoaFisica> PessoaFisica { get; set; }

        public void Validar()
        {
            new ValidationContract<EstadoCivil>(this)
                .IsRequired(x => x.EstadoCivilNome)
                .HasMaxLenght(x => x.EstadoCivilNome, 250);
            if (!IsValid())
            {
                throw new InvalidOperationException(GetMergeNotifications());
            }
        }
    }
}
