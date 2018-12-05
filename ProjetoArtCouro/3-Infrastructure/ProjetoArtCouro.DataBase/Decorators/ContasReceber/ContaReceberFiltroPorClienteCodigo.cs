using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltroPorClienteCodigo : IContaReceberFiltro
    {
        private readonly IContaReceberFiltro _contaReceberFiltro;

        public ContaReceberFiltroPorClienteCodigo(IContaReceberFiltro contaReceberFiltro)
        {
            _contaReceberFiltro = contaReceberFiltro;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            if (filtro.CodigoCliente == 0)
            {
                return _contaReceberFiltro.Filtrar(filtro);
            }

            return _contaReceberFiltro
                .Filtrar(filtro)
                .Where(x => x.Venda.Cliente.PessoaCodigo == filtro.CodigoCliente);
        }
    }
}
