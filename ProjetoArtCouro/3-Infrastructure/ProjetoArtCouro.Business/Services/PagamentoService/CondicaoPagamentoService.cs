﻿using System.Collections.Generic;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPagamento;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;
using AutoMapper;
using ProjetoArtCouro.Domain.Exceptions;

namespace ProjetoArtCouro.Business.Services.PagamentoService
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
            return Mapper.Map<List<CondicaoPagamentoModel>>(condicoesPagamentos);
        }

        public CondicaoPagamento ObterCondicaoPagamentoPorCodigo(int codigo)
        {
            return _condicaoPagamentoRepository.ObterPorCodigo(codigo);
        }

        public CondicaoPagamentoModel CriarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            var condicaoPagamento = Mapper.Map<CondicaoPagamento>(model);
            condicaoPagamento.Validar();

            var condicaoPagamentoIncluida = _condicaoPagamentoRepository
                .Criar(condicaoPagamento);

            return Mapper.Map<CondicaoPagamentoModel>(condicaoPagamentoIncluida);
        }

        public CondicaoPagamentoModel AtualizarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            var condicaoPagamento = Mapper.Map<CondicaoPagamento>(model);
            condicaoPagamento.Validar();

            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, condicaoPagamento.CondicaoPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "CondicaoPagamentoCodigo"));

            var condicaoPagamentoAtual =
                _condicaoPagamentoRepository.ObterPorCodigo(condicaoPagamento.CondicaoPagamentoCodigo);
            condicaoPagamentoAtual.Ativo = condicaoPagamento.Ativo;
            condicaoPagamentoAtual.Descricao = condicaoPagamento.Descricao;
            condicaoPagamentoAtual.QuantidadeParcelas = condicaoPagamento.QuantidadeParcelas;
            var condicaoPagamentoAtualizada = _condicaoPagamentoRepository
                .Atualizar(condicaoPagamentoAtual);

            return Mapper.Map<CondicaoPagamentoModel>(condicaoPagamentoAtualizada);
        }

        public void ExcluirCondicaoPagamento(int condicaoPagamentoCodigo)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(0, condicaoPagamentoCodigo,
                string.Format(Erros.NotZeroParameter, "CondicaoPagamentoCodigo"));

            var condicaoPagamentoAtual = _condicaoPagamentoRepository.ObterPorCodigo(condicaoPagamentoCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(condicaoPagamentoAtual, null, Erros.PaymentConditionDoesNotExist);

            _condicaoPagamentoRepository.Deletar(condicaoPagamentoAtual);
        }

        public void Dispose()
        {
            _condicaoPagamentoRepository.Dispose();
        }
    }
}
