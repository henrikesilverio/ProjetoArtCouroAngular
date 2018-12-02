using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public class VendaFiltroPorCodigo : IVendaFiltro
    {
        private readonly IVendaFiltro _vendaFiltro;

        public VendaFiltroPorCodigo(IVendaFiltro vendaFiltro)
        {
            _vendaFiltro = vendaFiltro;
        }

        public IQueryable<Venda> Filtrar(PesquisaVenda filtro)
        {
            if (filtro.CodigoVenda == 0)
            {
                return _vendaFiltro.Filtrar(filtro);
            }

            return _vendaFiltro
                .Filtrar(filtro)
                .Where(x => x.VendaCodigo == filtro.CodigoVenda);
        }
    }
}
