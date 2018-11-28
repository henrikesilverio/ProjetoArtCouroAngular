using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.DataBase.Repositorios.UsuarioRepository
{
    public class GrupoPermissaoRepository : IGrupoPermissaoRepository
    {
        private readonly DataBaseContext _context;

        public GrupoPermissaoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(GrupoPermissao gruposPermissao)
        {
            _context.GruposPermissao.Add(gruposPermissao);
            _context.SaveChanges();
        }

        public GrupoPermissao ObterPorId(Guid id)
        {
            return _context.GruposPermissao
                .FirstOrDefault(x => x.GrupoPermissaoId == id);
        }

        public GrupoPermissao ObterPorCodigo(int codigo)
        {
            return _context.GruposPermissao
                .FirstOrDefault(x => x.GrupoPermissaoCodigo == codigo);
        }

        public GrupoPermissao ObterPorCodigoComPermissoes(int codigo)
        {
            return _context.GruposPermissao
                .Include("Permissoes")
                .FirstOrDefault(x => x.GrupoPermissaoCodigo == codigo);
        }

        public GrupoPermissao ObterPorCodigoComPermissoesEUsuarios(int codigo)
        {
            return _context.GruposPermissao
                .Include("Permissoes")
                .Include("Usuarios")
                .FirstOrDefault(x => x.GrupoPermissaoCodigo == codigo);
        }

        public GrupoPermissao ObterPorNome(string nome)
        {
            return _context.GruposPermissao
                .FirstOrDefault(x => x.GrupoPermissaoNome == nome);
        }

        public List<GrupoPermissao> ObterLista()
        {
            return _context.GruposPermissao.ToList();
        }

        public List<GrupoPermissao> ObterListaForFiltro(PesquisaGrupoPermissao filtro)
        {
            var query = _context.GruposPermissao
                .Include("Permissoes")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro.GrupoNome))
            {
                query = query.Where(x => x.GrupoPermissaoNome == filtro.GrupoNome);
            }

            if (filtro.GrupoCodigo != 0)
            {
                query = query.Where(x => x.GrupoPermissaoCodigo == filtro.GrupoCodigo);
            }

            return query.ToList();
        }

        public void Atualizar(GrupoPermissao gruposPermissao)
        {
            _context.Entry(gruposPermissao).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(GrupoPermissao gruposPermissao)
        {
            _context.GruposPermissao.Remove(gruposPermissao);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
