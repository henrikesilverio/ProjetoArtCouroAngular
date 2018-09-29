using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ProjetoArtCouro.Api.Extensions;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Fornecedor;
using ProjetoArtCouro.Domain.Models.Pessoa;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pessoas
{
    [RoutePrefix("api/Fornecedor")]
    public class FornecedorController : BaseApiController
    {
        private readonly IPessoaService _pessoaService;
        public FornecedorController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route("CriarFornecedor")]
        [Authorize(Roles = "NovoFornecedor")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFornecedor")]
        [InvalidateCacheOutput("ObterListaFornecedor")]
        [InvalidateCacheOutput("ObterFornecedorPorCodigo")]
        [HttpPost]
        public IHttpActionResult CriarFornecedor(FornecedorModel model)
        {
            try
            {
                _pessoaService.CriarPessoa(model);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("PesquisarFornecedor")]
        [Authorize(Roles = "PesquisaFornecedor")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpPost]
        public IHttpActionResult PesquisarFornecedor(PesquisaPessoaModel model)
        {
            model.TipoPapelPessoa = TipoPapelPessoaEnum.Fornecedor;
            var pessoas = _pessoaService.PesquisarPessoa(model);
            return OkRetornoBase(pessoas);
        }

        [Route("ObterFornecedorPorCodigo/{codigoFornecedor:int:min(1)}")]
        [Authorize(Roles = "EditarFornecedor")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterFornecedorPorCodigo(int codigoFornecedor)
        {
            var pessoaModel = _pessoaService.ObterPessoaPorCodigo(codigoFornecedor);
            return OkRetornoBase(pessoaModel);
        }

        [Route("ObterListaFornecedor")]
        [Authorize(Roles = "NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaFornecedor()
        {
            try
            {
                var listaPessoa = _pessoaService.ObterListaPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum.Fornecedor);
                return OkRetornoBase(Mapper.Map<List<FornecedorModel>>(listaPessoa));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("EditarFornecedor")]
        [Authorize(Roles = "EditarFornecedor")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFornecedor")]
        [InvalidateCacheOutput("ObterListaFornecedor")]
        [InvalidateCacheOutput("ObterFornecedorPorCodigo")]
        [HttpPut]
        public IHttpActionResult EditarFornecedor(FornecedorModel model)
        {
            try
            {
                _pessoaService.AtualizarPessoa(model);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ExcluirFornecedor/{codigoFornecedor:int:min(1)}")]
        [Authorize(Roles = "ExcluirFornecedor")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFornecedor")]
        [InvalidateCacheOutput("ObterListaFornecedor")]
        [InvalidateCacheOutput("ObterFornecedorPorCodigo")]
        [HttpDelete]
        public IHttpActionResult ExcluirFornecedor(int codigoFornecedor)
        {
            try
            {
                _pessoaService.ExcluirPessoa(codigoFornecedor);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _pessoaService.Dispose();
        }
    }
}
