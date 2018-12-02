using System.Linq;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public class VendaFiltroPorDocumentoCliente : IVendaFiltro
    {
        private readonly IVendaFiltro _vendaFiltro;

        public VendaFiltroPorDocumentoCliente(IVendaFiltro vendaFiltro)
        {
            _vendaFiltro = vendaFiltro;
        }

        public IQueryable<Venda> Filtrar(PesquisaVenda filtro)
        {
            if (string.IsNullOrEmpty(filtro.CPFCNPJ))
            {
                return _vendaFiltro.Filtrar(filtro);
            }

            return _vendaFiltro
                .Filtrar(filtro)
                .Where(x => x.Cliente.PessoaFisica.CPF == filtro.CPFCNPJ ||
                            x.Cliente.PessoaJuridica.CNPJ == filtro.CPFCNPJ);
        }
    }
}
