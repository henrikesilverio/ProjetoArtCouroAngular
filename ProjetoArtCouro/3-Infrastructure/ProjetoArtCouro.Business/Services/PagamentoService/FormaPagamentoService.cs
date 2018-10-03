using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Models.FormaPagamento;
using AutoMapper;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Domain.Exceptions;

namespace ProjetoArtCouro.Business.Services.PagamentoService
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
            return Mapper.Map<List<FormaPagamentoModel>>(listaformaPagamento);
        }

        public FormaPagamento ObterFormaPagamentoPorCodigo(int codigo)
        {
            return _formaPagamentoRepository.ObterPorCodigo(codigo);
        }

        public FormaPagamentoModel CriarFormaPagamento(FormaPagamentoModel model)
        {
            var formaPagamento = Mapper.Map<FormaPagamento>(model);

            formaPagamento.Validar();
            var formaPagamentoIncluida = _formaPagamentoRepository.Criar(formaPagamento);

            return Mapper.Map<FormaPagamentoModel>(formaPagamentoIncluida);
        }

        public FormaPagamentoModel AtualizarFormaPagamento(FormaPagamentoModel model)
        {
            var formaPagamento = Mapper.Map<FormaPagamento>(model);
            formaPagamento.Validar();

            AssertionConcern<BusinessException>.AssertArgumentNotEquals(0, formaPagamento.FormaPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "FormaPagamentoCodigo"));

            var formaPagamentoAtual =
                _formaPagamentoRepository.ObterPorCodigo(formaPagamento.FormaPagamentoCodigo);

            formaPagamentoAtual.Ativo = formaPagamento.Ativo;
            formaPagamentoAtual.Descricao = formaPagamento.Descricao;
            var formaPagamentoAtualizada = _formaPagamentoRepository
                .Atualizar(formaPagamentoAtual);

            return Mapper.Map<FormaPagamentoModel>(formaPagamentoAtualizada);
        }

        public void ExcluirFormaPagamento(int formaPagamentoCodigo)
        {
            AssertionConcern<BusinessException>.AssertArgumentNotEquals(0, formaPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "FormaPagamentoCodigo"));

            var formaPagamentoAtual = _formaPagamentoRepository.ObterPorCodigo(formaPagamentoCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(formaPagamentoAtual, null, Erros.FormOfPaymentDoesNotExist);

            _formaPagamentoRepository.Deletar(formaPagamentoAtual);
        }

        public void Dispose()
        {
            _formaPagamentoRepository.Dispose();
        }
    }
}
