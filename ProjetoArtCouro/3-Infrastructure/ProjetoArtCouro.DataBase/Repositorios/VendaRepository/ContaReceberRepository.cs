using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Factories;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.DataBase.Repositorios.VendaRepository
{
    public class ContaReceberRepository : IContaReceberRepository
    {
        private readonly DataBaseContext _context;

        public ContaReceberRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(ContaReceber contaReceber)
        {
            _context.ContasReceber.Add(contaReceber);
            _context.SaveChanges();
        }

        public ContaReceber ObterPorCodigoComVenda(int codigo)
        {
            return _context.ContasReceber
                .Include("Venda")
                .FirstOrDefault(x => x.ContaReceberCodigo == codigo);
        }

        public List<ContaReceber> ObterListaPorVendaCodigo(int vendaCodigo)
        {
            return _context.ContasReceber
                .Include("Venda")
                .Where(x => x.Venda.VendaCodigo == vendaCodigo)
                .ToList();
        }

        public List<ContaReceber> ObterListaPorFiltro(PesquisaContaReceber filtro)
        {
            var contaReceberFiltro = ContaReceberFiltroFactory.Fabricar(_context);
            return contaReceberFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(ContaReceber contaReceber)
        {
            _context.Entry(contaReceber).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(ContaReceber contaReceber)
        {
            _context.ContasReceber.Remove(contaReceber);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
