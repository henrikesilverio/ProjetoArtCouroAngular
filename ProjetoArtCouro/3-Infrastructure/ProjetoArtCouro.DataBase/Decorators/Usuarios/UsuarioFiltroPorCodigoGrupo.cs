using System.Linq;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.Usuarios
{
    public class UsuarioFiltroPorCodigoGrupo : IUsuarioFiltro
    {
        private readonly IUsuarioFiltro _usuarioFiltro;

        public UsuarioFiltroPorCodigoGrupo(IUsuarioFiltro usuarioFiltro)
        {
            _usuarioFiltro = usuarioFiltro;
        }

        public IQueryable<Usuario> Filtrar(PesquisaUsuario filtro)
        {
            if (filtro.GrupoPermissaoCodigo == 0)
            {
                return _usuarioFiltro.Filtrar(filtro);
            }

            return _usuarioFiltro
                .Filtrar(filtro)
                .Where(x => x.GrupoPermissao.GrupoPermissaoCodigo == filtro.GrupoPermissaoCodigo);
        }
    }
}
