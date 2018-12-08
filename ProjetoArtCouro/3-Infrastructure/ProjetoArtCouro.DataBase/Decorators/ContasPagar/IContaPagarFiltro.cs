using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public interface IContaPagarFiltro
    {
        IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro);
    }
}
