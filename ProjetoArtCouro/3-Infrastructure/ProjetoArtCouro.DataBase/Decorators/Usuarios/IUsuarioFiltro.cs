using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.Usuarios
{
    public interface IUsuarioFiltro
    {
        IQueryable<Usuario> Filtrar(PesquisaUsuario filtro);
    }
}
