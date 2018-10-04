using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Usuarios
{
    public class Usuario : Notifiable
    {
        public Guid UsuarioId { get; set; }
        public int UsuarioCodigo { get; set; }
        public string UsuarioNome { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public virtual GrupoPermissao GrupoPermissao { get; set; }
        public virtual ICollection<Venda> Vendas { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Permissao> Permissoes { get; set; }

        public void Validar()
        {
            new ValidationContract<Usuario>(this)
                .IsRequired(x => x.UsuarioNome)
                .HasMaxLenght(x => x.UsuarioNome, 60)
                .IsRequired(x => x.Senha)
                .HasMaxLenght(x => x.Senha, 32)
                .IsRequired(x => x.Ativo);
            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
