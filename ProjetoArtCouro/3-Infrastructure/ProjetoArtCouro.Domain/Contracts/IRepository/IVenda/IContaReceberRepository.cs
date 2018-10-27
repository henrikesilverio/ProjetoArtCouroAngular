using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Vendas;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IVenda
{
    public interface IContaReceberRepository : IDisposable
    {
        ContaReceber ObterPorId(Guid id);
        ContaReceber ObterPorCodigo(int codigo);
        ContaReceber ObterPorCodigoComVenda(int codigo);
        List<ContaReceber> ObterLista();
        List<ContaReceber> ObterListaPorCodigoVenda(int codigoVenda);
        List<ContaReceber> ObterListaPorFiltro(PesquisaContaReceber filtro);
        void Criar(ContaReceber contaReceber);
        void Atualizar(ContaReceber contaReceber);
        void Deletar(ContaReceber contaReceber);
    }
}
