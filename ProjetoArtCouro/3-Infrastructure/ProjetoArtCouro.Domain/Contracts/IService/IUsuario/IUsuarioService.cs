using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.Domain.Contracts.IService.IUsuario
{
    public interface IUsuarioService : IDisposable
    {
        void CriarUsuario(UsuarioModel model);
        void AlterarSenha(string usuarioNome, string senha);
        void EditarUsuario(UsuarioModel usuario);
        void ExcluirUsuario(int codigoUsuario);
        List<Usuario> ObterListaUsuario();
        List<Permissao> ObterListaPermissao();
        List<UsuarioModel> PesquisarUsuario(PesquisaUsuarioModel model);
        UsuarioModel PesquisarUsuarioPorCodigo(int codigoUsuario);
        List<PermissaoModel> ObterPermissoesUsuarioLogado(string usuarioNome);
        GrupoPermissao ObterGrupoPermissaoPorCodigo(int codigo);
        List<GrupoPermissao> PesquisarGrupo(string nome, int? codigo, bool todos);
        List<GrupoPermissao> ObterListaGrupoPermissao();
        void CriarGrupoPermissao(GrupoPermissao grupoPermissao);
        void EditarGrupoPermissao(GrupoPermissao grupoPermissao);
        void EditarPermissaoUsuario(int codigoUsuario, List<Permissao> permissoes);
        void ExcluirGrupoPermissao(int codigoGrupoPermissao);
    }
}
