using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    class ContaPagarFiltroPorUsuarioCodigo : IContaPagarFiltro
    {
        private readonly IContaPagarFiltro _contaPagarFiltro;

        public ContaPagarFiltroPorUsuarioCodigo(IContaPagarFiltro contaPagarFiltro)
        {
            _contaPagarFiltro = contaPagarFiltro;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            if (filtro.CodigoUsuario == 0)
            {
                return _contaPagarFiltro.Filtrar(filtro);
            }

            return _contaPagarFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.Usuario.UsuarioCodigo == filtro.CodigoUsuario);
        }
    }
}
