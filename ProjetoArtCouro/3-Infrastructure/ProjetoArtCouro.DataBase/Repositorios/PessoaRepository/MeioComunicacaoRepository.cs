using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.DataBase.Repositorios.PessoaRepository
{
    public class MeioComunicacaoRepository : IMeioComunicacaoRepository
    {
        private readonly DataBaseContext _context;

        public MeioComunicacaoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public MeioComunicacao Criar(MeioComunicacao meioComunicacao)
        {
            _context.MeiosComunicacao.Add(meioComunicacao);
            _context.SaveChanges();
            return _context.Entry(meioComunicacao).Entity;
        }

        public MeioComunicacao ObterPorId(Guid id)
        {
            return _context.MeiosComunicacao.FirstOrDefault(x => x.MeioComunicacaoId == id);
        }

        public MeioComunicacao ObterPorCodigo(int codigo)
        {
            return _context.MeiosComunicacao.FirstOrDefault(x => x.MeioComunicacaoCodigo == codigo);
        }

        public List<MeioComunicacao> ObterLista()
        {
            return _context.MeiosComunicacao.ToList();
        }

        public void Atualizar(MeioComunicacao meioComunicacao)
        {
            _context.Entry(meioComunicacao).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(MeioComunicacao meioComunicacao)
        {
            _context.MeiosComunicacao.Remove(meioComunicacao);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
