using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.Estoques
{
    public interface IEstoqueFiltro
    {
        IQueryable<Estoque> Filtrar(PesquisaEstoque filtro);
    }
}
