using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public interface IVendaFiltro
    {
        IQueryable<Venda> Filtrar(PesquisaVenda filtro);
    }
}
