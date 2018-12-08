using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Estoques;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.DataBase.Decorators.Estoques
{
    public class EstoqueFiltro : IEstoqueFiltro
    {
        private readonly DataBaseContext _context;

        public EstoqueFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<Estoque> Filtrar(PesquisaEstoque filtro)
        {
            return _context.Estoques
                .Include("Produto")
                .Include("Compra")
                .Include("Compra.Fornecedor")
                .AsQueryable();
        }
    }
}
