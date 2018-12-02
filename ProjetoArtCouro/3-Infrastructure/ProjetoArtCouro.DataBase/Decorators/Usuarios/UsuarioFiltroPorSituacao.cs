using System.Linq;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.Usuarios
{
    public class UsuarioFiltroPorSituacao : IUsuarioFiltro
    {
        private readonly IUsuarioFiltro _usuarioFiltro;

        public UsuarioFiltroPorSituacao(IUsuarioFiltro usuarioFiltro)
        {
            _usuarioFiltro = usuarioFiltro;
        }

        public IQueryable<Usuario> Filtrar(PesquisaUsuario filtro)
        {
            if (filtro.Ativo == null)
            {
                return _usuarioFiltro.Filtrar(filtro);
            }

            return _usuarioFiltro
                .Filtrar(filtro)
                .Where(x => x.Ativo == filtro.Ativo);
        }
    }
}
