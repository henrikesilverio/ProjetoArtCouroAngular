using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.Domain.Contracts.IService.IVenda
{
    public interface IContaReceberService : IDisposable
    {
        List<ContaReceberModel> PesquisarContaReceber(int codigoUsuario, PesquisaContaReceberModel model);
        void ReceberContas(List<ContaReceberModel> model);
    }
}
