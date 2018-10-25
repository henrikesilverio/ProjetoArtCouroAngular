using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IVenda
{
    public interface IVendaRepository : IDisposable
    {
        Venda ObterPorId(Guid id);
        Venda ObterPorCodigo(int codigo);
        Venda ObterPorCodigoComItensVenda(int codigo);
        List<Venda> ObterLista();
        List<Venda> ObterListaPorFiltro(PesquisaVenda filtro);
        void Criar(Venda venda);
        void Atualizar(Venda venda);
        void Deletar(Venda venda);
    }
}
