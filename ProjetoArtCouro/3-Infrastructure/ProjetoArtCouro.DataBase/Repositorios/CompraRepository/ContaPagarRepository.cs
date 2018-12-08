using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Factories;
using ProjetoArtCouro.Domain.Contracts.IRepository.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.DataBase.Repositorios.CompraRepository
{
    public class ContaPagarRepository : IContaPagarRepository
    {
        private readonly DataBaseContext _context;

        public ContaPagarRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(ContaPagar contaPagar)
        {
            _context.ContasPagar.Add(contaPagar);
            _context.SaveChanges();
        }

        public ContaPagar ObterPorCodigoComCompra(int codigo)
        {
            return _context.ContasPagar
                .Include("Compra")
                .FirstOrDefault(x => x.ContaPagarCodigo == codigo);
        }

        public List<ContaPagar> ObterListaPorCompraCodigo(int compraCodigo)
        {
            return _context.ContasPagar
                .Include("Compra")
                .Where(x => x.Compra.CompraCodigo == compraCodigo)
                .ToList();
        }

        public List<ContaPagar> ObterListaPorFiltro(PesquisaContaPagar filtro)
        {
            var contaPagarFiltro = ContaPagarFiltroFactory.Fabricar(_context);
            return contaPagarFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(ContaPagar contaPagar)
        {
            _context.Entry(contaPagar).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(ContaPagar contaPagar)
        {
            _context.ContasPagar.Remove(contaPagar);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
