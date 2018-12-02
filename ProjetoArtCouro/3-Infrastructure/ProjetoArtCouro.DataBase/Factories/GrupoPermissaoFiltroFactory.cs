using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.GruposPermissoes;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class GrupoPermissaoFiltroFactory
    {
        public static IGrupoPermissaoFiltro Fabricar(
            DataBaseContext context)
        {
            IGrupoPermissaoFiltro filtro = new GrupoPermissaoFiltro(context);

            filtro = new GrupoPermissaoFiltroPorNome(filtro);
            filtro = new GrupoPermissaoFiltroPorCodigo(filtro);

            return filtro;
        }
    }
}
