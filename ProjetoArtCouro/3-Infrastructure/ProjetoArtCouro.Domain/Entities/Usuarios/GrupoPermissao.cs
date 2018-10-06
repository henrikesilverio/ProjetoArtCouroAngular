using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;
using System;
using System.Collections.Generic;

namespace ProjetoArtCouro.Domain.Entities.Usuarios
{
    public class GrupoPermissao : Notifiable
    {
        public Guid GrupoPermissaoId { get; set; }
        public int GrupoPermissaoCodigo { get; set; }
        public string GrupoPermissaoNome { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Permissao> Permissoes { get; set; }

        public void Validar()
        {
            new ValidationContract<GrupoPermissao>(this)
                .IsRequired(x => x.GrupoPermissaoNome)
                .HasMaxLenght(x => x.GrupoPermissaoNome, 50)
                .IsNotNull(x => x.Permissoes, Erros.EmptyAllowList);

            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
