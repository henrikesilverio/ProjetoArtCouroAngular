using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pagamentos
{
    [RoutePrefix("api/CondicaoPagamento")]
    public class CondicaoPagamentoController : BaseApiController
    {
        private readonly ICondicaoPagamentoService _condicaoPagamentoService;
        public CondicaoPagamentoController(ICondicaoPagamentoService condicaoPagamentoService)
        {
            _condicaoPagamentoService = condicaoPagamentoService;
        }

        [Route("ObterListaCondicaoPagamento")]
        [Authorize(Roles = "PesquisaCondicaoPagamento, NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaCondicaoPagamento()
        {
            try
            {
                var listaCondicaoPagamento = _condicaoPagamentoService.ObterListaCondicaoPagamento();
                return OkRetornoBase(Mapper.Map<List<CondicaoPagamentoModel>>(listaCondicaoPagamento));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("CriarCondicaoPagamento")]
        [Authorize(Roles = "NovaCondicaoPagamento")]
        [InvalidateCacheOutput("ObterListaCondicaoPagamento")]
        [HttpPost]
        public IHttpActionResult CriarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            try
            {
                var condicaoPagamento = Mapper.Map<CondicaoPagamento>(model);
                var condicaoPagamentoRetorno = _condicaoPagamentoService.CriarCondicaoPagamento(condicaoPagamento);
                return OkRetornoBase(Mapper.Map<CondicaoPagamentoModel>(condicaoPagamentoRetorno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("EditarCondicaoPagamento")]
        [Authorize(Roles = "EditarCondicaoPagamento")]
        [InvalidateCacheOutput("ObterListaCondicaoPagamento")]
        [HttpPut]
        public IHttpActionResult EditarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            try
            {
                var condicaoPagamento = Mapper.Map<CondicaoPagamento>(model);
                var condicaoPagamentoRetorno = _condicaoPagamentoService.AtualizarCondicaoPagamento(condicaoPagamento);
                return OkRetornoBase(Mapper.Map<CondicaoPagamentoModel>(condicaoPagamentoRetorno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ExcluirCondicaoPagamento/{codigoCondicaoPagamento:int:min(1)}")]
        [Authorize(Roles = "ExcluirCondicaoPagamento")]
        [InvalidateCacheOutput("ObterListaCondicaoPagamento")]
        [HttpDelete]
        public IHttpActionResult ExcluirCondicaoPagamento(int codigoCondicaoPagamento)
        {
            try
            {
                _condicaoPagamentoService.ExcluirCondicaoPagamento(codigoCondicaoPagamento);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _condicaoPagamentoService.Dispose();
        }
    }
}
