using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Factories;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.DataBase.Repositorios.CompraRepository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly DataBaseContext _context;

        public CompraRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Compra compra)
        {
            _context.Compras.Add(compra);
            _context.SaveChanges();
        }

        public Compra ObterPorCodigo(int codigo)
        {
            return _context.Compras
                .FirstOrDefault(x => x.CompraCodigo == codigo);
        }

        public Compra ObterPorCodigoComItensCompra(int codigo)
        {
            return _context.Compras
                .Include("ItensCompra")
                .FirstOrDefault(x => x.CompraCodigo == codigo);
        }

        public List<Compra> ObterListaPorFiltro(PesquisaCompra filtro)
        {
            var compraFiltro = CompraFiltroFactory.Fabricar(_context);
            return compraFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(Compra compra)
        {
            _context.Entry(compra).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Compra compra)
        {
            _context.Compras.Remove(compra);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
