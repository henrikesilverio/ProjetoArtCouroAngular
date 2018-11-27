using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario
{
    public interface IUsuarioRepository : IDisposable
    {
        Usuario ObterPorId(Guid id);
        Usuario ObterPorCodigo(int codigo);
        Usuario ObterPorCodigoComPermissoes(int codigo);
        Usuario ObterPorCodigoComPermissoesEGrupo(int codigo);
        Usuario ObterPorUsuarioNome(string usuarioNome);
        Usuario ObterPorUsuarioNomeComPermissoes(string usuarioNome);
        Usuario ObterPorUsuarioNomeComPermissoesEGrupo(string usuarioNome);
        List<Usuario> ObterLista();
        List<Usuario> ObterListaComPermissoes();
        List<Usuario> ObterListaPorFiltro(PesquisaUsuario filtro);
        void Criar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Deletar(Usuario usuario);
    }
}
