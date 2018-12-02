using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Decorators.Usuarios;

namespace ProjetoArtCouro.DataBase.Factories
{
    public static class UsuarioFiltroFactory
    {
        public static IUsuarioFiltro Fabricar(
            DataBaseContext context)
        {
            IUsuarioFiltro filtro = new UsuarioFiltro(context);

            filtro = new UsuarioFiltroPorNome(filtro);
            filtro = new UsuarioFiltroPorSituacao(filtro);
            filtro = new UsuarioFiltroPorCodigoGrupo(filtro);

            return filtro;
        }
    }
}
