using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Decorators.Vendas
{
    public class VendaFiltro : IVendaFiltro
    {
        private readonly DataBaseContext _context;

        public VendaFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<Venda> Filtrar(PesquisaVenda filtro)
        {
            return _context.Vendas
                .Include("Usuario")
                .Include("ItensVenda")
                .Include("Cliente")
                .Include("Cliente.PessoaFisica")
                .Include("Cliente.PessoaJuridica")
                .AsQueryable();
        }
    }
}
