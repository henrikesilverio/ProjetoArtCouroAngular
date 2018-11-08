using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Domain.Entities.Usuarios
{
    public class Permissao : Notifiable
    {
        public Guid PermissaoId { get; set; }
        public int PermissaoCodigo { get; set; }
        public string PermissaoNome { get; set; }
        public string AcaoNome { get; set; }
        public virtual ICollection<GrupoPermissao> GrupoPermissao { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public void Validar()
        {
            new ValidationContract<Permissao>(this)
                .IsRequired(x => x.PermissaoNome)
                .HasMaxLenght(x => x.PermissaoNome, 50)
                .IsRequired(x => x.AcaoNome)
                .HasMaxLenght(x => x.AcaoNome, 50);

            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
