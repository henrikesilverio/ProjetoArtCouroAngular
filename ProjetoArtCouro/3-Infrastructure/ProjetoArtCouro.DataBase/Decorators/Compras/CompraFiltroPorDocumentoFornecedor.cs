using System.Linq;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltroPorDocumentoFornecedor : ICompraFiltro
    {
        private readonly ICompraFiltro _compraFiltro;

        public CompraFiltroPorDocumentoFornecedor(ICompraFiltro compraFiltro)
        {
            _compraFiltro = compraFiltro;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            if (string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                return _compraFiltro.Filtrar(filtro);
            }

            return _compraFiltro
                .Filtrar(filtro)
                .Where(x => x.Fornecedor.PessoaFisica.CPF == filtro.CPFCNPJ ||
                            x.Fornecedor.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
        }
    }
}
