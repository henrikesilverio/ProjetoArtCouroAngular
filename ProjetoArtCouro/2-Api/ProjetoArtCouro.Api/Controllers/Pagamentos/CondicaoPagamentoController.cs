using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
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

        [Route("CriarCondicaoPagamento")]
        [Authorize(Roles = "NovaCondicaoPagamento")]
        [InvalidateCacheOutput("ObterListaCondicaoPagamento")]
        [HttpPost]
        public IHttpActionResult CriarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            var condicaoPagamentoModel = _condicaoPagamentoService.CriarCondicaoPagamento(model);
            return OkRetornoBase(condicaoPagamentoModel);
        }

        [Route("ObterListaCondicaoPagamento")]
        [Authorize(Roles = "PesquisaCondicaoPagamento, NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaCondicaoPagamento()
        {
            var listaCondicaoPagamentoModel = _condicaoPagamentoService.ObterListaCondicaoPagamento();
            return OkRetornoBase(listaCondicaoPagamentoModel);
        }

        [Route("EditarCondicaoPagamento")]
        [Authorize(Roles = "EditarCondicaoPagamento")]
        [InvalidateCacheOutput("ObterListaCondicaoPagamento")]
        [HttpPut]
        public IHttpActionResult EditarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            var condicaoPagamentoModel = _condicaoPagamentoService.AtualizarCondicaoPagamento(model);
            return OkRetornoBase(condicaoPagamentoModel);
        }

        [Route("ExcluirCondicaoPagamento/{codigoCondicaoPagamento:int:min(1)}")]
        [Authorize(Roles = "ExcluirCondicaoPagamento")]
        [InvalidateCacheOutput("ObterListaCondicaoPagamento")]
        [HttpDelete]
        public IHttpActionResult ExcluirCondicaoPagamento(int codigoCondicaoPagamento)
        {
            _condicaoPagamentoService.ExcluirCondicaoPagamento(codigoCondicaoPagamento);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _condicaoPagamentoService.Dispose();
        }
    }
}
