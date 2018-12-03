using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Vendas;

namespace ProjetoArtCouro.DataBase.Repositorios.VendaRepository
{
    public class ItemVendaRepository : IItemVendaRepository
    {
        private readonly DataBaseContext _context;

        public ItemVendaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public ItemVenda Criar(ItemVenda itemVenda)
        {
            _context.ItensVenda.Add(itemVenda);
            _context.SaveChanges();
            return _context.Entry(itemVenda).Entity;
        }

        public void Deletar(ItemVenda itemVenda)
        {
            _context.ItensVenda.Remove(itemVenda);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
