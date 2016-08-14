using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pagamentos;

namespace ProjetoArtCouro.Domain.Contracts.IService.IPagamento
{
    public interface ICondicaoPagamentoService : IDisposable
    {
        List<CondicaoPagamento> ObterListaCondicaoPagamento();
        CondicaoPagamento ObterCondicaoPagamentoPorCodigo(int codigo);
        CondicaoPagamento CriarCondicaoPagamento(CondicaoPagamento condicaoPagamento);
        CondicaoPagamento AtualizarCondicaoPagamento(CondicaoPagamento condicaoPagamento);
        void ExcluirCondicaoPagamento(int condicaoPagamentoCodigo);
    }
}
