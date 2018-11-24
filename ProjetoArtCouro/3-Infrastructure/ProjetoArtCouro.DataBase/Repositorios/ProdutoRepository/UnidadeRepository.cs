using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IProduto;
using ProjetoArtCouro.Domain.Entities.Produtos;

namespace ProjetoArtCouro.DataBase.Repositorios.ProdutoRepository
{
    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly DataBaseContext _context;

        public UnidadeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Unidade unidade)
        {
            _context.Unidades.Add(unidade);
            _context.SaveChanges();
        }

        public Unidade ObterPorId(Guid id)
        {
            return _context.Unidades.FirstOrDefault(x => x.UnidadeId == id);
        }

        public Unidade ObterPorCodigo(int codigo)
        {
            return _context.Unidades.FirstOrDefault(x => x.UnidadeCodigo == codigo);
        }

        public List<Unidade> ObterLista()
        {
            return _context.Unidades.AsNoTracking().ToList();
        }

        public void Atualizar(Unidade unidade)
        {
            _context.Entry(unidade).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Unidade unidade)
        {
            _context.Unidades.Remove(unidade);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
