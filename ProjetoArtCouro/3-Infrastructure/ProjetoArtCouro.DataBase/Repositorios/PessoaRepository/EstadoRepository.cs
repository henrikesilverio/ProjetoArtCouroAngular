using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly DataBaseContext _context;

        public EstadoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Estado estado)
        {
            _context.Estados.Add(estado);
            _context.SaveChanges();
        }

        public Estado ObterPorId(Guid id)
        {
            return _context.Estados.FirstOrDefault(x => x.EstadoId == id);
        }

        public Estado ObterPorCodigo(int codigo)
        {
            return _context.Estados.FirstOrDefault(x => x.EstadoCodigo == codigo);
        }

        public List<Estado> ObterLista()
        {
            return _context.Estados.ToList();
        }

        public void Atualizar(Estado estado)
        {
            _context.Entry(estado).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Estado estado)
        {
            _context.Estados.Remove(estado);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
