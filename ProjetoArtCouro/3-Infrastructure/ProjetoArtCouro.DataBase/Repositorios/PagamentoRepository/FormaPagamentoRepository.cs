using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;

namespace ProjetoArtCouro.DataBase.Repositorios.PagamentoRepository
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly DataBaseContext _context;

        public FormaPagamentoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public FormaPagamento Criar(FormaPagamento formaPagamento)
        {
            _context.FormasPagamento.Add(formaPagamento);
            _context.SaveChanges();
            return _context.Entry(formaPagamento).Entity;
        }

        public FormaPagamento ObterPorCodigo(int codigo)
        {
            return _context.FormasPagamento
                .FirstOrDefault(x => x.FormaPagamentoCodigo == codigo);
        }

        public List<FormaPagamento> ObterLista()
        {
            return _context.FormasPagamento
                .AsNoTracking().ToList();
        }

        public FormaPagamento Atualizar(FormaPagamento formaPagamento)
        {
            _context.Entry(formaPagamento).State = EntityState.Modified;
            _context.SaveChanges();
            return _context.Entry(formaPagamento).Entity;
        }

        public void Deletar(FormaPagamento formaPagamento)
        {
            _context.FormasPagamento.Remove(formaPagamento);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
