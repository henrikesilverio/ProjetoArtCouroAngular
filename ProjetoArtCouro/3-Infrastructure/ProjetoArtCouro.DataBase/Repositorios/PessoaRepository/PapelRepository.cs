using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class PapelRepository : IPapelRepository
    {
        private readonly DataBaseContext _context;

        public PapelRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Papel papel)
        {
            _context.Papeis.Add(papel);
            _context.SaveChanges();
        }

        public Papel ObterPorId(Guid id)
        {
            return _context.Papeis.FirstOrDefault(x => x.PapelId == id);
        }

        public Papel ObterPorCodigo(int codigo)
        {
            return _context.Papeis.FirstOrDefault(x => x.PapelCodigo == codigo);
        }

        public List<Papel> ObterLista()
        {
            return _context.Papeis.ToList();
        }

        public void Atualizar(Papel papel)
        {
            _context.Entry(papel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Papel papel)
        {
            _context.Papeis.Remove(papel);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
