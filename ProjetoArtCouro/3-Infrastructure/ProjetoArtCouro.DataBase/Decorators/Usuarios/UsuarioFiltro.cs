using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.Usuarios
{
    public class UsuarioFiltro : IUsuarioFiltro
    {
        private readonly DataBaseContext _context;

        public UsuarioFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<Usuario> Filtrar(PesquisaUsuario filtro)
        {
            return _context.Usuarios
                .Include("GrupoPermissao")
                .AsQueryable();
        }
    }
}
