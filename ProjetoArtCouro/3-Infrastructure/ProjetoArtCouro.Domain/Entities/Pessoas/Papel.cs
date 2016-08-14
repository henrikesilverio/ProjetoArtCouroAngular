using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class Papel
    {
        public Guid PapelId { get; set; }
        public int PapelCodigo { get; set; }
        public string PapelNome { get; set; }
        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}
