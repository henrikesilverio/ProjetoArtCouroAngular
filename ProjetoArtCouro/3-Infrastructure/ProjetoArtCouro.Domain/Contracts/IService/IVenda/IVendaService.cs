using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.Domain.Contracts.IService.IVenda
{
    public interface IVendaService : IDisposable
    {
        void CriarVenda(int usuarioCodigo, VendaModel model);
        List<VendaModel> PesquisarVenda(int codigoUsuario, PesquisaVendaModel model);
        VendaModel ObterVendaPorCodigo(int codigoVenda);
        void AtualizarVenda(VendaModel model);
        void ExcluirVenda(int codigoVenda);
    }
}
