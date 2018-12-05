using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltroPorVendaCodigo : IContaReceberFiltro
    {
        private readonly IContaReceberFiltro _contaReceberFiltro;

        public ContaReceberFiltroPorVendaCodigo(IContaReceberFiltro contaReceberFiltro)
        {
            _contaReceberFiltro = contaReceberFiltro;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            if (filtro.CodigoVenda == 0)
            {
                return _contaReceberFiltro.Filtrar(filtro);
            }
            
            return _contaReceberFiltro
                .Filtrar(filtro)
                .Where(x => x.Venda.VendaCodigo == filtro.CodigoVenda);
        }
    }
}
