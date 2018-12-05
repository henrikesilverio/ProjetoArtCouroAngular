using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IVenda
{
    public interface IContaReceberRepository : IDisposable
    {
        ContaReceber ObterPorCodigoComVenda(int codigo);
        List<ContaReceber> ObterListaPorVendaCodigo(int vendaCodigo);
        List<ContaReceber> ObterListaPorFiltro(PesquisaContaReceber filtro);
        void Criar(ContaReceber contaReceber);
        void Atualizar(ContaReceber contaReceber);
        void Deletar(ContaReceber contaReceber);
    }
}
