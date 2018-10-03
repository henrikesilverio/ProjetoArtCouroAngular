using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;

namespace ProjetoArtCouro.Domain.Contracts.IService.IPagamento
{
    public interface ICondicaoPagamentoService : IDisposable
    {
        List<CondicaoPagamentoModel> ObterListaCondicaoPagamento();
        CondicaoPagamento ObterCondicaoPagamentoPorCodigo(int codigo);
        CondicaoPagamentoModel CriarCondicaoPagamento(CondicaoPagamentoModel model);
        CondicaoPagamentoModel AtualizarCondicaoPagamento(CondicaoPagamentoModel model);
        void ExcluirCondicaoPagamento(int condicaoPagamentoCodigo);
    }
}
