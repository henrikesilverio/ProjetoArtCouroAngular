using System.Linq;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.GruposPermissoes
{
    public class GrupoPermissaoFiltroPorCodigo : IGrupoPermissaoFiltro
    {
        private readonly IGrupoPermissaoFiltro _grupoPermissaoFiltro;

        public GrupoPermissaoFiltroPorCodigo(IGrupoPermissaoFiltro grupoPermissaoFiltro)
        {
            _grupoPermissaoFiltro = grupoPermissaoFiltro;
        }

        public IQueryable<GrupoPermissao> Filtrar(PesquisaGrupoPermissao filtro)
        {
            if (filtro.GrupoCodigo == 0)
            {
                return _grupoPermissaoFiltro.Filtrar(filtro);
            }

            return _grupoPermissaoFiltro
                .Filtrar(filtro)
                .Where(x => x.GrupoPermissaoCodigo == filtro.GrupoCodigo);
        }
    }
}
