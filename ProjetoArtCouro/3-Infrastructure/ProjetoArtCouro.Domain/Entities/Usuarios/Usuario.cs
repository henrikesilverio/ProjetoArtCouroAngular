﻿using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Usuarios
{
    public class Usuario
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
            //AssertionConcern.AssertArgumentLength(UsuarioNome, 3, 250, Erros.InvalidUserName);
            //PasswordAssertionConcern.AssertIsValid(Senha);
        }
    }
}
