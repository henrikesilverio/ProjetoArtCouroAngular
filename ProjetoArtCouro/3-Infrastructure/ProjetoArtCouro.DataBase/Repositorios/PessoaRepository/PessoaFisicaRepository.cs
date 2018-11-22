using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using System.Data.Entity;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.DataBase.Factories;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class PessoaFisicaRepository : IPessoaFisicaRepository
    {
        private readonly DataBaseContext _context;

        public PessoaFisicaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(PessoaFisica pessoaFisica)
        {
            _context.PessoasFisicas.Add(pessoaFisica);
            _context.SaveChanges();
        }

        public PessoaFisica ObterPorId(Guid id)
        {
            return _context.PessoasFisicas.FirstOrDefault(x => x.PessoaId == id);
        }

        public PessoaFisica ObterPorCPF(string cpf)
        {
            return _context.PessoasFisicas.FirstOrDefault(x => x.CPF == cpf);
        }

        public List<PessoaFisica> ObterLista()
        {
            return _context.PessoasFisicas.AsNoTracking().ToList();
        }

        public List<PessoaFisica> ObterListaPorFiltro(PesquisaPessoaFisica filtro)
        {
            var pessoaFisicaFiltro = PessoaFisicaFiltroFactory.Fabricar(_context);
            return pessoaFisicaFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(PessoaFisica pessoaFisica)
        {
            _context.Entry(pessoaFisica).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(PessoaFisica pessoaFisica)
        {
            _context.PessoasFisicas.Remove(pessoaFisica);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
