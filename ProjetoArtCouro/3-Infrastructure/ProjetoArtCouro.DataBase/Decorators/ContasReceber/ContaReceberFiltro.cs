using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Decorators.ContasReceber
{
    public class ContaReceberFiltro : IContaReceberFiltro
    {
        private readonly DataBaseContext _context;

        public ContaReceberFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<ContaReceber> Filtrar(PesquisaContaReceber filtro)
        {
            return _context.ContasReceber
                .Include("Venda")
                .Include("Venda.Usuario")
                .Include("Venda.Cliente.PessoaFisica")
                .Include("Venda.Cliente.PessoaJuridica")
                .AsQueryable();
        }
    }
}
