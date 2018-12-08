using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;

namespace ProjetoArtCouro.DataBase.Repositorios.CompraRepository
{
    public class ItemCompraRepository : IItemCompraRepository
    {
        private readonly DataBaseContext _context;

        public ItemCompraRepository(DataBaseContext context)
        {
            _context = context;
        }

        public ItemCompra Criar(ItemCompra itemCompra)
        {
            _context.ItensCompra.Add(itemCompra);
            _context.SaveChanges();
            return _context.Entry(itemCompra).Entity;
        }

        public void Deletar(ItemCompra itemCompra)
        {
            _context.ItensCompra.Remove(itemCompra);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
