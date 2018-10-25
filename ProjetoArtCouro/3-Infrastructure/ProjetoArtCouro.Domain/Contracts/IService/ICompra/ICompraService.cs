using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.Domain.Contracts.IService.ICompra
{
    public interface ICompraService : IDisposable
    {
        void CriarCompra(int usuarioCodigo, CompraModel compra);
        List<CompraModel> PesquisarCompra(int codigoUsuario, PesquisaCompraModel model);
        CompraModel ObterCompraPorCodigo(int codigoCompra);
        void AtualizarCompra(CompraModel compra);
        void ExcluirCompra(int codigoCompra);
    }
}
