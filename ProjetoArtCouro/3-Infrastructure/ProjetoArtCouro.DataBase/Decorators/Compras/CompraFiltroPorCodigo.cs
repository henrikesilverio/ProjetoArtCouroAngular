using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltroPorCodigo : ICompraFiltro
    {
        private readonly ICompraFiltro _compraFiltro;

        public CompraFiltroPorCodigo(ICompraFiltro compraFiltro)
        {
            _compraFiltro = compraFiltro;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            if (filtro.CodigoCompra == 0)
            {
                return _compraFiltro.Filtrar(filtro);
            }

            return _compraFiltro
                .Filtrar(filtro)
                .Where(x => x.CompraCodigo == filtro.CodigoCompra);
        }
    }
}
