using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.GruposPermissoes
{
    public interface IGrupoPermissaoFiltro
    {
        IQueryable<GrupoPermissao> Filtrar(PesquisaGrupoPermissao filtro);
    }
}
