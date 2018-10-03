using System;
using System.Collections.Generic;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Models.FormaPagamento;

namespace ProjetoArtCouro.Domain.Contracts.IService.IPagamento
{
    public interface IFormaPagamentoService : IDisposable
    {
        List<FormaPagamentoModel> ObterListaFormaPagamento();
        FormaPagamento ObterFormaPagamentoPorCodigo(int codigo);
        FormaPagamentoModel CriarFormaPagamento(FormaPagamentoModel model);
        FormaPagamentoModel AtualizarFormaPagamento(FormaPagamentoModel model);
        void ExcluirFormaPagamento(int formaPagamentoCodigo);
    }
}
