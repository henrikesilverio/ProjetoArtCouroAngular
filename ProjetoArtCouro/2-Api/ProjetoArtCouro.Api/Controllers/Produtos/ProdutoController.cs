using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IProduto;
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

        [Route("CriarProduto")]
        [Authorize(Roles = "NovoProduto")]
        [InvalidateCacheOutput("ObterListaProduto")]
        [HttpPost]
        public IHttpActionResult CriarProduto(ProdutoModel model)
        {
            var produtoIncluido = _produtoService.CriarProduto(model);
            return OkRetornoBase(produtoIncluido);
        }

        [Route("ObterListaProduto")]
        [Authorize(Roles = "PesquisaProduto, NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaProduto()
        {
            var produtos = _produtoService.ObterListaProduto();
            return OkRetornoBase(produtos);
        }

        [Route("ObterListaUnidade")]
        [Authorize(Roles = "PesquisaProduto")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaUnidade()
        {
            var unidades = _produtoService.ObterListaUnidade();
            return OkRetornoBase(unidades);
        }

        [Route("EditarProduto")]
        [Authorize(Roles = "EditarProduto")]
        [InvalidateCacheOutput("ObterListaProduto")]
        [HttpPut]
        public IHttpActionResult EditarProduto(ProdutoModel model)
        {
            var produtoRetorno = _produtoService.AtualizarProduto(model);
            return OkRetornoBase(produtoRetorno);
        }

        [Route("ExcluirProduto/{codigoProduto:int:min(1)}")]
        [Authorize(Roles = "ExcluirProduto")]
        [InvalidateCacheOutput("ObterListaProduto")]
        [HttpDelete]
        public IHttpActionResult ExcluirProduto(int codigoProduto)
        {
            _produtoService.ExcluirProduto(codigoProduto);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _produtoService.Dispose();
        }
    }
}
