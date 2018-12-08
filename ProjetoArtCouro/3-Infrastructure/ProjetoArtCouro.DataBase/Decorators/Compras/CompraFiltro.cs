using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Decorators.Compras
{
    public class CompraFiltro : ICompraFiltro
    {
        private readonly DataBaseContext _context;

        public CompraFiltro(DataBaseContext context)
        {
            _context = context;
        }

        public IQueryable<Compra> Filtrar(PesquisaCompra filtro)
        {
            return _context.Compras
                .Include("Usuario")
                .Include("ItensCompra")
                .Include("Fornecedor")
                .Include("Fornecedor.PessoaFisica")
                .Include("Fornecedor.PessoaJuridica")
                .AsQueryable();
        }
    }
}
