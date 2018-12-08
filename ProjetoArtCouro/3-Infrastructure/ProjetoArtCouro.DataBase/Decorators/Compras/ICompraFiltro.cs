using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public interface ICompraFiltro
    {
        IQueryable<Compra> Filtrar(PesquisaCompra filtro);
    }
}
