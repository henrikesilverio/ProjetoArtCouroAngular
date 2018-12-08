using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using System.Linq;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public class ContaPagarFiltroPorDocumentoFornecedor : IContaPagarFiltro
    {
        private readonly IContaPagarFiltro _contaPagarFiltro;

        public ContaPagarFiltroPorDocumentoFornecedor(IContaPagarFiltro contaPagarFiltro)
        {
            _contaPagarFiltro = contaPagarFiltro;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            if (string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                return _contaPagarFiltro.Filtrar(filtro);
            }

            return _contaPagarFiltro
                .Filtrar(filtro)
                .Where(x => x.Compra.Fornecedor.PessoaFisica.CPF == filtro.CPFCNPJ ||
                            x.Compra.Fornecedor.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
        }
    }
}
