using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Entities.Pagamentos;
using ProjetoArtCouro.Domain.Models.FormaPagamento;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pagamentos
{
    [RoutePrefix("api/FormaPagamento")]
    public class FormaPagamentoController : BaseApiController
    {
        private readonly IFormaPagamentoService _formaPagamentoService;
        public FormaPagamentoController(IFormaPagamentoService formaPagamentoService)
        {
            _formaPagamentoService = formaPagamentoService;
        }

        [Route("ObterListaFormaPagamento")]
        [Authorize(Roles = "PesquisaFormaPagamento, NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaFormaPagamento()
        {
            try
            {
                var listaformaPagamento = _formaPagamentoService.ObterListaFormaPagamento();
                return OkRetornoBase(Mapper.Map<List<FormaPagamentoModel>>(listaformaPagamento));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("CriarFormaPagamento")]
        [Authorize(Roles = "NovaFormaPagamento")]
        [InvalidateCacheOutput("ObterListaFormaPagamento")]
        [HttpPost]
        public IHttpActionResult CriarFormaPagamento(FormaPagamentoModel model)
        {
            try
            {
                var formaPagamento = Mapper.Map<FormaPagamento>(model);
                var formaPagamentoRetorno = _formaPagamentoService.CriarFormaPagamento(formaPagamento);
                return OkRetornoBase(Mapper.Map<FormaPagamentoModel>(formaPagamentoRetorno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("EditarFormaPagamento")]
        [Authorize(Roles = "EditarFormaPagamento")]
        [InvalidateCacheOutput("ObterListaFormaPagamento")]
        [HttpPut]
        public IHttpActionResult EditarFormaPagamento(FormaPagamentoModel model)
        {
            try
            {
                var formaPagamento = Mapper.Map<FormaPagamento>(model);
                var formaPagamentoRetorno = _formaPagamentoService.AtualizarFormaPagamento(formaPagamento);
                return OkRetornoBase(Mapper.Map<FormaPagamentoModel>(formaPagamentoRetorno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ExcluirFormaPagamento/{codigoFormaPagamento:int:min(1)}")]
        [Authorize(Roles = "ExcluirFormaPagamento")]
        [InvalidateCacheOutput("ObterListaFormaPagamento")]
        [HttpDelete]
        public IHttpActionResult ExcluirFormaPagamento(int codigoFormaPagamento)
        {
            try
            {
                _formaPagamentoService.ExcluirFormaPagamento(codigoFormaPagamento);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _formaPagamentoService.Dispose();
        }
    }
}
