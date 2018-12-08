using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltroPorCodigoUsuario : ICompraFiltro
    {
        private readonly ICompraFiltro _compraFiltro;

        public CompraFiltroPorCodigoUsuario(ICompraFiltro compraFiltro)
        {
            _compraFiltro = compraFiltro;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            if (filtro.CodigoUsuario == 0)
            {
                return _compraFiltro.Filtrar(filtro);
            }

            return _compraFiltro
                .Filtrar(filtro)
                .Where(x => x.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
        }
    }
}
