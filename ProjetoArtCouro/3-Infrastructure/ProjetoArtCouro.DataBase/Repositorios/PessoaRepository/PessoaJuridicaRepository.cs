using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Data.Entity;
using ProjetoArtCouro.DataBase.Factories;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        private readonly DataBaseContext _context;

        public PessoaJuridicaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(PessoaJuridica pessoaJuridica)
        {
            _context.PessoasJuridicas.Add(pessoaJuridica);
            _context.SaveChanges();
        }

        public PessoaJuridica ObterPorId(Guid id)
        {
            return _context.PessoasJuridicas.FirstOrDefault(x => x.PessoaId.Equals(id));
        }

        public PessoaJuridica ObterPorCNPJ(string cnpj)
        {
            return _context.PessoasJuridicas.FirstOrDefault(x => x.CNPJ.Equals(cnpj));
        }

        public List<PessoaJuridica> ObterLista()
        {
            return _context.PessoasJuridicas.AsNoTracking().ToList();
        }

        public List<PessoaJuridica> ObterListaPorFiltro(PesquisaPessoaJuridica filtro)
        {
            var pessoaJuridicaFiltro = PessoaJuridicaFiltroFactory.Fabricar(_context);
            return pessoaJuridicaFiltro.Filtrar(filtro).ToList();
        }

        public void Atualizar(PessoaJuridica pessoaJuridica)
        {
            _context.Entry(pessoaJuridica).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(PessoaJuridica pessoaJuridica)
        {
            _context.PessoasJuridicas.Remove(pessoaJuridica);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
