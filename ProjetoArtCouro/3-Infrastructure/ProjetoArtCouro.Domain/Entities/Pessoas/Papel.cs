using System;
using System.Collections.Generic;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class Papel : Notifiable
    {
        public Guid PapelId { get; set; }
        public int PapelCodigo { get; set; }
        public string PapelNome { get; set; }
        public virtual ICollection<Pessoa> Pessoas { get; set; }

        public void Validar()
        {
            new ValidationContract<Papel>(this)
                .IsRequired(x => x.PapelNome)
                .HasMaxLenght(x => x.PapelNome, 250);
            if (!IsValid())
            {
                throw new InvalidOperationException(GetMergeNotifications());
            }
        }
    }
}
