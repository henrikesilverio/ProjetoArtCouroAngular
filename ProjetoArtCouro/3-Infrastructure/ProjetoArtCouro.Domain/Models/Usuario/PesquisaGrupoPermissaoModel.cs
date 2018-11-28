using System.ComponentModel.DataAnnotations;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Domain.Models.Usuario
{
    public class PesquisaGrupoPermissaoModel
    {
        [Display(Name = "GroupCode", ResourceType = typeof(Mensagens))]
        public int GrupoCodigo { get; set; }

        [Display(Name = "GroupName", ResourceType = typeof(Mensagens))]
        public string GrupoNome { get; set; }

        [Display(Name = "EveryoneGroup", ResourceType = typeof(Mensagens))]
        public bool Todos { get; set; }
    }
}
