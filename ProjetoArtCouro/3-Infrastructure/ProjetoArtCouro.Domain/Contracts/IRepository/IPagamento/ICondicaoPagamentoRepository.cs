using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pagamentos;

namespace ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento
{
    public interface ICondicaoPagamentoRepository : IDisposable
    {
        CondicaoPagamento ObterPorCodigo(int codigo);
        List<CondicaoPagamento> ObterLista();
        CondicaoPagamento Criar(CondicaoPagamento condicaoPagamento);
        CondicaoPagamento Atualizar(CondicaoPagamento condicaoPagamento);
        void Deletar(CondicaoPagamento condicaoPagamento);
    }
}
