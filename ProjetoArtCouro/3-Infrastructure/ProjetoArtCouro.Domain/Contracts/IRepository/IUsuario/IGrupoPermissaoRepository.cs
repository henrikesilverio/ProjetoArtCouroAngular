using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Models.Usuario;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario
{
    public interface IGrupoPermissaoRepository : IDisposable
    {
        GrupoPermissao ObterPorId(Guid id);
        GrupoPermissao ObterPorCodigo(int codigo);
        GrupoPermissao ObterPorCodigoComPermissoes(int codigo);
        GrupoPermissao ObterPorCodigoComPermissoesEUsuarios(int codigo);
        GrupoPermissao ObterPorNome(string nome);
        List<GrupoPermissao> ObterLista();
        List<GrupoPermissao> ObterListaForFiltro(PesquisaGrupoPermissao filtro);
        void Criar(GrupoPermissao gruposPermissao);
        void Atualizar(GrupoPermissao gruposPermissao);
        void Deletar(GrupoPermissao gruposPermissao);
    }
}
