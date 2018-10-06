using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.Domain.Contracts.IService.IUsuario
{
    public interface IUsuarioService : IDisposable
    {
        void CriarUsuario(UsuarioModel model);
        void AlterarSenha(string usuarioNome, string senha);
        void EditarUsuario(UsuarioModel usuario);
        void ExcluirUsuario(int codigoUsuario);
        List<UsuarioModel> ObterListaUsuario();
        List<PermissaoModel> ObterListaPermissao();
        List<UsuarioModel> PesquisarUsuario(PesquisaUsuarioModel model);
        UsuarioModel PesquisarUsuarioPorCodigo(int codigoUsuario);
        List<PermissaoModel> ObterPermissoesUsuarioLogado(string usuarioNome);
        GrupoModel ObterGrupoPermissaoPorCodigo(int codigoGrupo);
        List<GrupoModel> PesquisarGrupo(PesquisaGrupoModel model);
        List<GrupoModel> ObterListaGrupoPermissao();
        void CriarGrupoPermissao(GrupoModel model);
        void EditarGrupoPermissao(GrupoModel model);
        void EditarPermissaoUsuario(ConfiguracaoUsuarioModel model);
        void ExcluirGrupoPermissao(int codigoGrupoPermissao);
    }
}
