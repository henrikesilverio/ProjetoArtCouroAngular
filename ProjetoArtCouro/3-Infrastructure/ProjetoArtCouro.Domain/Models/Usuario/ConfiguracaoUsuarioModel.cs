﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Domain.Models.Usuario
{
    public class ConfiguracaoUsuarioModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Erros))]
        [Display(Name = "Users", ResourceType = typeof(Mensagens))]
        public int UsuarioCodigo { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Erros))]
        [Display(Name = "Permissions", ResourceType = typeof(Mensagens))]
        public string PermissaoId { get; set; }

        public List<PermissaoModel> Permissoes { get; set; }
    }
}
