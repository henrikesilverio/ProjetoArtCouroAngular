using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public class ContaPagarFiltroPorCodigoFornecedor : IContaPagarFiltro
    {
        private readonly IContaPagarFiltro _contaPagarFiltro;

        public ContaPagarFiltroPorCodigoFornecedor(IContaPagarFiltro contaPagarFiltro)
        {
            _contaPagarFiltro = contaPagarFiltro;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            if (filtro.CodigoFornecedor == 0)
            {
                return _contaPagarFiltro.Filtrar(filtro);
            }

            return _contaPagarFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.Fornecedor.PessoaCodigo == filtro.CodigoFornecedor);
        }
    }
}
