using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Usuarios;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario
{
    public interface IPermissaoRepository : IDisposable
    {
        Permissao ObterPermissaoPorCodigo(int codigo);
        List<Permissao> ObterLista();
    }
}
