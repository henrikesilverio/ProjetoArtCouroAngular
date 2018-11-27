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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataBaseContext _context;

        public UsuarioRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Criar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario ObterPorId(Guid id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.UsuarioId == id);
        }

        public Usuario ObterPorCodigo(int codigo)
        {
            return _context.Usuarios.FirstOrDefault(x => x.UsuarioCodigo == codigo);
        }

        public Usuario ObterPorCodigoComPermissoes(int codigo)
        {
            return _context.Usuarios
                .Include("Permissoes")
                .FirstOrDefault(x => x.UsuarioCodigo == codigo);
        }

        public Usuario ObterPorCodigoComPermissoesEGrupo(int codigo)
        {
            return _context.Usuarios
                .Include("Permissoes")
                .Include("GrupoPermissao")
                .FirstOrDefault(x => x.UsuarioCodigo == codigo);
        }

        public Usuario ObterPorUsuarioNome(string usuarioNome)
        {
            return _context.Usuarios
                .FirstOrDefault(x => x.UsuarioNome == usuarioNome);
        }

        public Usuario ObterPorUsuarioNomeComPermissoes(string usuarioNome)
        {
            return _context.Usuarios
                .Include("Permissoes")
                .FirstOrDefault(x => x.UsuarioNome == usuarioNome);
        }

        public Usuario ObterPorUsuarioNomeComPermissoesEGrupo(string usuarioNome)
        {
            return _context.Usuarios
                .Include("Permissoes")
                .Include("GrupoPermissao")
                .Include("GrupoPermissao.Permissoes")
                .FirstOrDefault(x => x.UsuarioNome == usuarioNome);
        }

        public List<Usuario> ObterLista()
        {
            return _context.Usuarios.ToList();
        }

        public List<Usuario> ObterListaComPermissoes()
        {
            return _context.Usuarios
                .Include("Permissoes")
                .ToList();
        }

        public List<Usuario> ObterListaPorFiltro(PesquisaUsuario filtro)
        {
            var query = _context.Usuarios
                .Include("GrupoPermissao")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro.UsuarioNome))
            {
                query = query.Where(x => x.UsuarioNome == filtro.UsuarioNome);
            }

            if (filtro.GrupoPermissaoCodigo != 0)
            {
                query = query.Where(x => x.GrupoPermissao.GrupoPermissaoCodigo == filtro.GrupoPermissaoCodigo);
            }

            if (filtro.Ativo != null)
            {
                query = query.Where(x => x.Ativo == filtro.Ativo);
            }

            return query.ToList();
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
