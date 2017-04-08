using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IProduto;
using ProjetoArtCouro.Domain.Entities.Produtos;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Produto;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Produtos
{
    [RoutePrefix("api/Produto")]
    public class ProdutoController : BaseApiController
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [Route("ObterListaProduto")]
        [Authorize(Roles = "PesquisaProduto, NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaProduto()
        {
            try
            {
                var listaProduto = _produtoService.ObterListaProduto();
                return OkRetornoBase(Mapper.Map<List<ProdutoModel>>(listaProduto));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ObterListaUnidade")]
        [Authorize(Roles = "PesquisaProduto")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaUnidade()
        {
            try
            {
                var listaUnidade = _produtoService.ObterListaUnidade();
                return OkRetornoBase(Mapper.Map<List<LookupModel>>(listaUnidade));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("CriarProduto")]
        [Authorize(Roles = "NovoProduto")]
        [InvalidateCacheOutput("ObterListaProduto")]
        [HttpPost]
        public IHttpActionResult CriarProduto(ProdutoModel model)
        {
            try
            {
                var produto = Mapper.Map<Produto>(model);
                var produtoRetorno = _produtoService.CriarProduto(produto);
                return OkRetornoBase(Mapper.Map<ProdutoModel>(produtoRetorno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("EditarProduto")]
        [Authorize(Roles = "EditarProduto")]
        [InvalidateCacheOutput("ObterListaProduto")]
        [HttpPut]
        public IHttpActionResult EditarProduto(ProdutoModel model)
        {
            try
            {
                var produto = Mapper.Map<Produto>(model);
                var produtoRetorno = _produtoService.AtualizarProduto(produto);
                return OkRetornoBase(Mapper.Map<ProdutoModel>(produtoRetorno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ExcluirProduto/{codigoProduto:int:min(1)}")]
        [Authorize(Roles = "ExcluirProduto")]
        [InvalidateCacheOutput("ObterListaProduto")]
        [HttpDelete]
        public IHttpActionResult ExcluirProduto(int codigoProduto)
        {
            try
            {
                _produtoService.ExcluirProduto(codigoProduto);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _produtoService.Dispose();
        }
    }
}
