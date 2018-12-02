using System.Linq;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.GruposPermissoes
{
    public class GrupoPermissaoFiltroPorNome : IGrupoPermissaoFiltro
    {
        private readonly IGrupoPermissaoFiltro _grupoPermissaoFiltro;

        public GrupoPermissaoFiltroPorNome(IGrupoPermissaoFiltro grupoPermissaoFiltro)
        {
            _grupoPermissaoFiltro = grupoPermissaoFiltro;
        }

        public IQueryable<GrupoPermissao> Filtrar(PesquisaGrupoPermissao filtro)
        {
            if (string.IsNullOrEmpty(filtro.GrupoNome))
            {
                return _grupoPermissaoFiltro.Filtrar(filtro);
            }

            return _grupoPermissaoFiltro
                .Filtrar(filtro)
                .Where(x => x.GrupoPermissaoNome == filtro.GrupoNome);
        }
    }
}
