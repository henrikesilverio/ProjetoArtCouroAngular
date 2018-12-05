using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public interface IContaReceberFiltro
    {
        IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro);
    }
}
