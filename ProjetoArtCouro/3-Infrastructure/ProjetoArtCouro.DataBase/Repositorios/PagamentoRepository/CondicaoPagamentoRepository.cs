﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;

namespace ProjetoArtCouro.DataBase.Repositorios.PagamentoRepository
{
    public class CondicaoPagamentoRepository : ICondicaoPagamentoRepository
    {
        private readonly DataBaseContext _context;

        public CondicaoPagamentoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public CondicaoPagamento Criar(CondicaoPagamento condicaoPagamento)
        {
            _context.CondicoesPagamento.Add(condicaoPagamento);
            _context.SaveChanges();
            return _context.Entry(condicaoPagamento).Entity;
        }

        public CondicaoPagamento ObterPorCodigo(int codigo)
        {
            return _context.CondicoesPagamento
                .FirstOrDefault(x => x.CondicaoPagamentoCodigo == codigo);
        }

        public List<CondicaoPagamento> ObterLista()
        {
            return _context.CondicoesPagamento
                .AsNoTracking().ToList();
        }

        public CondicaoPagamento Atualizar(CondicaoPagamento condicaoPagamento)
        {
            _context.Entry(condicaoPagamento).State = EntityState.Modified;
            _context.SaveChanges();
            return _context.Entry(condicaoPagamento).Entity;
        }

        public void Deletar(CondicaoPagamento condicaoPagamento)
        {
            _context.CondicoesPagamento.Remove(condicaoPagamento);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
