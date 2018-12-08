using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.DataBase.Decorators.ContasPagar
{
    public class ContaPagarFiltro : IContaPagarFiltro
    {
        private readonly DataBaseContext _context;

        public ContaPagarFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<ContaPagar> Filtrar(PesquisaContaPagar filtro)
        {
            return _context.ContasPagar
                .Include("Compra")
                .Include("Compra.Usuario")
                .Include("Compra.Fornecedor.PessoaFisica")
                .Include("Compra.Fornecedor.PessoaJuridica")
                .AsQueryable();
        }
    }
}
