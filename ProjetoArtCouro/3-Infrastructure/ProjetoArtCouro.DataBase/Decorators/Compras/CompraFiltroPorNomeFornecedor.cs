using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltroPorNomeFornecedor : ICompraFiltro
    {
        private readonly ICompraFiltro _compraFiltro;

        public CompraFiltroPorNomeFornecedor(ICompraFiltro compraFiltro)
        {
            _compraFiltro = compraFiltro;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            if (string.IsNullOrEmpty(filtro.NomeFornecedor))
            {
                return _compraFiltro.Filtrar(filtro);
            }

            return _compraFiltro
                .Filtrar(filtro)
                .Where(x => x.Fornecedor.Nome == filtro.NomeFornecedor);
        }
    }
}
