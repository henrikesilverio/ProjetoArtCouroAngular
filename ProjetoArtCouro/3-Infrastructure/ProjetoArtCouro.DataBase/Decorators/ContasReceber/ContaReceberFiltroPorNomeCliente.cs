using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltroPorNomeCliente : IContaReceberFiltro
    {
        private readonly IContaReceberFiltro _contaReceberFiltro;

        public ContaReceberFiltroPorNomeCliente(IContaReceberFiltro contaReceberFiltro)
        {
            _contaReceberFiltro = contaReceberFiltro;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            if (string.IsNullOrEmpty(filtro.NomeCliente))
            {
                return _contaReceberFiltro.Filtrar(filtro);
            }

            return _contaReceberFiltro
                .Filtrar(filtro)
                .Where(x => x.Venda.Cliente.Nome == filtro.NomeCliente);
        }
    }
}
