using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using System.Data.Entity;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DataBaseContext _context;

        public PessoaRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }

        public Pessoa ObterPorId(Guid id)
        {
            return _context.Pessoas.FirstOrDefault(x => x.PessoaId == id);
        }

        public Pessoa ObterPorCodigo(int codigo)
        {
            return _context.Pessoas.FirstOrDefault(x => x.PessoaCodigo == codigo);
        }

        public Pessoa ObterPorCodigoComPessoaCompleta(int codigo)
        {
            return _context.Pessoas
                .Include("PessoaFisica")
                .Include("PessoaFisica.EstadoCivil")
                .Include("PessoaJuridica")
                .Include("Papeis")
                .Include("MeiosComunicacao")
                .Include("Enderecos")
                .Include("Enderecos.Estado")
                .FirstOrDefault(x => x.PessoaCodigo == codigo);
        }

        public Pessoa ObterPorCPFComPessoaCompleta(string cpf)
        {
            return _context.Pessoas
                .Include("PessoaFisica")
                .Include("PessoaFisica.EstadoCivil")
                .Include("Papeis")
                .Include("MeiosComunicacao")
                .Include("Enderecos")
                .Include("Enderecos.Estado")
                .FirstOrDefault(x => x.PessoaFisica.CPF == cpf);
        }

        public Pessoa ObterPorCNPJComPessoaCompleta(string cnpj)
        {
            return _context.Pessoas
                .Include("PessoaJuridica")
                .Include("Papeis")
                .Include("MeiosComunicacao")
                .Include("Enderecos")
                .Include("Enderecos.Estado")
                .FirstOrDefault(x => x.PessoaJuridica.CNPJ == cnpj);
        }

        public List<Pessoa> ObterListaComPessoaFisicaEJuridica()
        {
            return _context.Pessoas
                .Include("PessoaFisica")
                .Include("PessoaJuridica")
                .AsNoTracking()
                .ToList();
        }

        public List<Pessoa> ObterListaComPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            return _context.Pessoas
                .Include("Papeis")
                .Include("PessoaFisica")
                .Include("PessoaJuridica")
                .Where(x => x.Papeis.Any(a => a.PapelCodigo == (int)papelCodigo))
                .AsNoTracking()
                .ToList();
        }

        public void Atualizar(Pessoa pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Pessoa pessoa)
        {
            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
