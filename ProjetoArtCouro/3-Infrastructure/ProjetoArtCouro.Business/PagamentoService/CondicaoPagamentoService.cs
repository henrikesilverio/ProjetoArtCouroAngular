using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.PagamentoService
{
    public class CondicaoPagamentoService : ICondicaoPagamentoService
    {
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;

        public CondicaoPagamentoService(ICondicaoPagamentoRepository condicaoPagamentoRepository)
        {
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
        }

        public List<CondicaoPagamentoModel> ObterListaCondicaoPagamento()
        {
            var condicoesPagamentos = _condicaoPagamentoRepository.ObterLista();
            return Map<List<CondicaoPagamentoModel>>.MapperTo(condicoesPagamentos);
        }

        public CondicaoPagamento ObterCondicaoPagamentoPorCodigo(int codigo)
        {
            return _condicaoPagamentoRepository.ObterPorCodigo(codigo);
        }

        public CondicaoPagamentoModel CriarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            var condicaoPagamento = Map<CondicaoPagamento>.MapperTo(model);
            condicaoPagamento.Validar();

            var condicaoPagamentoIncluida = _condicaoPagamentoRepository
                .Criar(condicaoPagamento);

            return Map<CondicaoPagamentoModel>.MapperTo(condicaoPagamentoIncluida);
        }

        public CondicaoPagamentoModel AtualizarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            var condicaoPagamento = Map<CondicaoPagamento>.MapperTo(model);
            condicaoPagamento.Validar();

            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, condicaoPagamento.CondicaoPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "CondicaoPagamentoCodigo"));

            var condicaoPagamentoAtual = _condicaoPagamentoRepository
                .ObterPorCodigo(condicaoPagamento.CondicaoPagamentoCodigo);

            condicaoPagamentoAtual.Ativo = condicaoPagamento.Ativo;
            condicaoPagamentoAtual.Descricao = condicaoPagamento.Descricao;
            condicaoPagamentoAtual.QuantidadeParcelas = condicaoPagamento.QuantidadeParcelas;

            var condicaoPagamentoAtualizada = _condicaoPagamentoRepository
                .Atualizar(condicaoPagamentoAtual);

            return Map<CondicaoPagamentoModel>.MapperTo(condicaoPagamentoAtualizada);
        }

        public void ExcluirCondicaoPagamento(int condicaoPagamentoCodigo)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, condicaoPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "CondicaoPagamentoCodigo"));

            var condicaoPagamentoAtual = _condicaoPagamentoRepository
                .ObterPorCodigo(condicaoPagamentoCodigo);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(condicaoPagamentoAtual, Erros.PaymentConditionDoesNotExist);

            _condicaoPagamentoRepository.Deletar(condicaoPagamentoAtual);
        }

        public void Dispose()
        {
            _condicaoPagamentoRepository.Dispose();
        }
    }
}
