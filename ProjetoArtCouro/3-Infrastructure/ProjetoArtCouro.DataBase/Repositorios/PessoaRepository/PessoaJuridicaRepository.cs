using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Data.Entity;

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
            var query = _context.PessoasJuridicas
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

            if (!string.IsNullOrEmpty(filtro.CNPJ))
            {
                query = query.Where(x => x.CNPJ == filtro.CNPJ);
            }

            if (!string.IsNullOrEmpty(filtro.Email))
            {
                query = query.Where(x => x.Pessoa.MeiosComunicacao
                    .Any(a => a.TipoComunicacao == TipoComunicacaoEnum.Email && a.MeioComunicacaoNome == filtro.Email));
            }

            return query.ToList();
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
