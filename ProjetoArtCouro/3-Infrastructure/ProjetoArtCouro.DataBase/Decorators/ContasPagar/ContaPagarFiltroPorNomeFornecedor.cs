using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public class ContaPagarFiltroPorNomeFornecedor : IContaPagarFiltro
    {
        private readonly IContaPagarFiltro _contaPagarFiltro;

        public ContaPagarFiltroPorNomeFornecedor(IContaPagarFiltro contaPagarFiltro)
        {
            _contaPagarFiltro = contaPagarFiltro;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            if (string.IsNullOrEmpty(filtro.NomeFornecedor))
            {
                return _contaPagarFiltro.Filtrar(filtro);
            }

            return _contaPagarFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.Fornecedor.Nome == filtro.NomeFornecedor);
        }
    }
}
