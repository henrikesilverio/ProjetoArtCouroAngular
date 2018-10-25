using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Models.Compra;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.ICompra
{
    public interface ICompraRepository : IDisposable
    {
        Compra ObterPorId(Guid id);
        Compra ObterPorCodigo(int codigo);
        Compra ObterPorCodigoComItensCompra(int codigo);
        List<Compra> ObterLista();
        List<Compra> ObterListaPorFiltro(PesquisaCompra filtro);
        void Criar(Compra compra);
        void Atualizar(Compra compra);
        void Deletar(Compra compra);
    }
}
