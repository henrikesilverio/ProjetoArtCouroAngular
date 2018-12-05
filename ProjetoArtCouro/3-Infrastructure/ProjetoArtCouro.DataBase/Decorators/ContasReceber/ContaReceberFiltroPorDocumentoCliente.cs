using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltroPorDocumentoCliente : IContaReceberFiltro
    {
        private readonly IContaReceberFiltro _contaReceberFiltro;

        public ContaReceberFiltroPorDocumentoCliente(IContaReceberFiltro contaReceberFiltro)
        {
            _contaReceberFiltro = contaReceberFiltro;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            if (string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                return _contaReceberFiltro.Filtrar(filtro);
            }

            return _contaReceberFiltro
                .Filtrar(filtro)
                .Where(x => x.Venda.Cliente.PessoaFisica.CPF == filtro.CPFCNPJ ||
                            x.Venda.Cliente.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
        }
    }
}
