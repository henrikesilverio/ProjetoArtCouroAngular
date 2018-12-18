using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Models.FormaPagamento;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.PagamentoService
{
    public class FormaPagamentoService : IFormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;

        public FormaPagamentoService(IFormaPagamentoRepository formaPagamentoRepository)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
        }

        public List<FormaPagamentoModel> ObterListaFormaPagamento()
        {
            var listaformaPagamento = _formaPagamentoRepository.ObterLista();
            return Map<List<FormaPagamentoModel>>.MapperTo(listaformaPagamento);
        }

        public FormaPagamento ObterFormaPagamentoPorCodigo(int codigo)
        {
            return _formaPagamentoRepository.ObterPorCodigo(codigo);
        }

        public FormaPagamentoModel CriarFormaPagamento(FormaPagamentoModel model)
        {
            var formaPagamento = Map<FormaPagamento>.MapperTo(model);
            formaPagamento.Validar();

            var formaPagamentoIncluida = _formaPagamentoRepository.Criar(formaPagamento);

            return Map<FormaPagamentoModel>.MapperTo(formaPagamentoIncluida);
        }

        public FormaPagamentoModel AtualizarFormaPagamento(FormaPagamentoModel model)
        {
            var formaPagamento = Map<FormaPagamento>.MapperTo(model);
            formaPagamento.Validar();

            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, formaPagamento.FormaPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "FormaPagamentoCodigo"));

            var formaPagamentoAtual = _formaPagamentoRepository
                .ObterPorCodigo(formaPagamento.FormaPagamentoCodigo);

            formaPagamentoAtual.Ativo = formaPagamento.Ativo;
            formaPagamentoAtual.Descricao = formaPagamento.Descricao;

            var formaPagamentoAtualizada = _formaPagamentoRepository
                .Atualizar(formaPagamentoAtual);

            return Map<FormaPagamentoModel>.MapperTo(formaPagamentoAtualizada);
        }

        public void ExcluirFormaPagamento(int formaPagamentoCodigo)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, formaPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "FormaPagamentoCodigo"));

            var formaPagamentoAtual = _formaPagamentoRepository
                .ObterPorCodigo(formaPagamentoCodigo);

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(formaPagamentoAtual, Erros.FormOfPaymentDoesNotExist);

            _formaPagamentoRepository.Deletar(formaPagamentoAtual);
        }

        public void Dispose()
        {
            _formaPagamentoRepository.Dispose();
        }
    }
}
