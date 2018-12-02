using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Decorators.GruposPermissoes
{
    public class GrupoPermissaoFiltro : IGrupoPermissaoFiltro
    {
        private readonly DataBaseContext _context;

        public GrupoPermissaoFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<GrupoPermissao> Filtrar(PesquisaGrupoPermissao filtro)
        {
            return _context.GruposPermissao
                .Include("Permissoes")
                .AsQueryable();
        }
    }
}
