using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Factories;
using ProjetoArtCouro.Domain.Contracts.IRepository.IVenda;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.DataBase.Repositorios.VendaRepository
{
    public class VendaRepository : IVendaRepository
    {
        private readonly DataBaseContext _context;

        public VendaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
        }

        public Venda ObterPorId(Guid id)
        {
            return _context.Vendas
                .FirstOrDefault(x => x.VendaId == id);
        }

        public Venda ObterPorCodigo(int codigo)
        {
            return _context.Vendas
                .FirstOrDefault(x => x.VendaCodigo == codigo);
        }

        public Venda ObterPorCodigoComItensVenda(int codigo)
        {
            return _context.Vendas
                .Include("ItensVenda")
                .FirstOrDefault(x => x.VendaCodigo == codigo);
        }

        public List<Venda> ObterLista()
        {
            return _context.Vendas.ToList();
        }

        public List<Venda> ObterListaPorFiltro(PesquisaVenda filtro)
        {
            var vendaFiltro = VendaFiltroFactory.Fabricar(_context);
            return vendaFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(Venda venda)
        {
            _context.Entry(venda).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Venda venda)
        {
            _context.Vendas.Remove(venda);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
