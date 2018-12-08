using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;
using ProjetoArtCouro.Domain.Models.Enums;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltroPorStatusCompra : ICompraFiltro
    {
        private readonly ICompraFiltro _compraFiltro;

        public CompraFiltroPorStatusCompra(ICompraFiltro compraFiltro)
        {
            _compraFiltro = compraFiltro;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            if (filtro.StatusCompra == StatusCompraEnum.None)
            {
                return _compraFiltro.Filtrar(filtro);
            }

            return _compraFiltro
                .Filtrar(filtro)
                .Where(x => x.StatusCompra == filtro.StatusCompra);
        }
    }
}
