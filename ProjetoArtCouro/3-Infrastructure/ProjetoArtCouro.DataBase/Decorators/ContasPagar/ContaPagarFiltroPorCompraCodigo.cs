using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public class ContaPagarFiltroPorCompraCodigo : IContaPagarFiltro
    {
        private readonly IContaPagarFiltro _contaPagarFiltro;

        public ContaPagarFiltroPorCompraCodigo(IContaPagarFiltro contaPagarFiltro)
        {
            _contaPagarFiltro = contaPagarFiltro;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            if (filtro.CodigoCompra == 0)
            {
                return _contaPagarFiltro.Filtrar(filtro);
            }
            
            return _contaPagarFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.CompraCodigo == filtro.CodigoCompra);
        }
    }
}
