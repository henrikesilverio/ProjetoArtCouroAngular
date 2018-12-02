using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public class VendaFiltroPorCodigoUsuario : IVendaFiltro
    {
        private readonly IVendaFiltro _vendaFiltro;

        public VendaFiltroPorCodigoUsuario(IVendaFiltro vendaFiltro)
        {
            _vendaFiltro = vendaFiltro;
        }

        public IQueryable<Venda> Filtrar(PesquisaVenda filtro)
        {
            if (filtro.CodigoUsuario == 0)
            {
                return _vendaFiltro.Filtrar(filtro);
            }

            return _vendaFiltro
                .Filtrar(filtro)
                .Where(x => x.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
        }
    }
}
