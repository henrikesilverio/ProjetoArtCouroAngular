using System.Linq;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.Usuarios
{
    public class UsuarioFiltroPorNome : IUsuarioFiltro
    {
        private readonly IUsuarioFiltro _usuarioFiltro;

        public UsuarioFiltroPorNome(IUsuarioFiltro usuarioFiltro)
        {
            _usuarioFiltro = usuarioFiltro;
        }

        public IQueryable<Usuario> Filtrar(PesquisaUsuario filtro)
        {
            if (string.IsNullOrEmpty(filtro.UsuarioNome))
            {
                return _usuarioFiltro.Filtrar(filtro);
            }

            return _usuarioFiltro
                .Filtrar(filtro)
                .Where(x => x.UsuarioNome == filtro.UsuarioNome);
        }
    }
}
