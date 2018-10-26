using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.Domain.Contracts.IService.ICompra
{
    public interface IContaPagarService : IDisposable
    {
        List<ContaPagarModel> PesquisarContaPagar(int codigoUsuario, PesquisaContaPagarModel model);
        void PagarContas(List<ContaPagarModel> model);
    }
}
