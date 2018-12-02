using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public class VendaFiltroPorNomeCliente : IVendaFiltro
    {
        private readonly IVendaFiltro _vendaFiltro;

        public VendaFiltroPorNomeCliente(IVendaFiltro vendaFiltro)
        {
            _vendaFiltro = vendaFiltro;
        }

        public IQueryable<Venda> Filtrar(PesquisaVenda filtro)
        {
            if (string.IsNullOrEmpty(filtro.NomeCliente))
            {
                return _vendaFiltro.Filtrar(filtro);
            }

            return _vendaFiltro
                .Filtrar(filtro)
                .Where(x => x.Cliente.Nome == filtro.NomeCliente);
        }
    }
}
