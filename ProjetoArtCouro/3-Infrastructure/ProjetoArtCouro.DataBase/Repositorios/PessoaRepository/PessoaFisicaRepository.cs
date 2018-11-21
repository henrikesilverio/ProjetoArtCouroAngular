using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using System.Data.Entity;
using ProjetoArtCouro.Domain.Models.Pessoa;

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
            var query = _context.PessoasFisicas
                    .Include("Pessoa")
                    .Include("Pessoa.Papeis")
                    .Include("Pessoa.MeiosComunicacao")
                    .Include("Pessoa.Enderecos")
                    .AsNoTracking()
                    .AsQueryable();

            if (!filtro.Codigo.Equals(0))
            {
                query = query.Where(x => x.Pessoa.PessoaCodigo == filtro.Codigo);
            }

            if (filtro.TipoPapelPessoa != TipoPapelPessoaEnum.Nenhum)
            {
                query = query.Where(x => x.Pessoa.Papeis.Any(a => a.PapelCodigo == (int)filtro.TipoPapelPessoa));
            }

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                query = query.Where(x => x.Pessoa.Nome == filtro.Nome);
            }

            if (!string.IsNullOrEmpty(filtro.CPF))
            {
                query = query.Where(x => x.CPF == filtro.CPF);
            }

            if (!string.IsNullOrEmpty(filtro.Email))
            {
                query = query
                    .Where(x => x.Pessoa.MeiosComunicacao
                    .Any(a => a.TipoComunicacao == TipoComunicacaoEnum.Email && a.MeioComunicacaoNome == filtro.Email));
            }

            return query.ToList();
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
