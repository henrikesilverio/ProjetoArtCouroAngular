using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
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

        [Route("CriarFormaPagamento")]
        [Authorize(Roles = "NovaFormaPagamento")]
        [InvalidateCacheOutput("ObterListaFormaPagamento")]
        [HttpPost]
        public IHttpActionResult CriarFormaPagamento(FormaPagamentoModel model)
        {
            var formaPagamentoModel = _formaPagamentoService.CriarFormaPagamento(model);
            return OkRetornoBase(formaPagamentoModel);
        }

        [Route("ObterListaFormaPagamento")]
        [Authorize(Roles = "PesquisaFormaPagamento, NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaFormaPagamento()
        {
            var listaformaPagamentoModel = _formaPagamentoService.ObterListaFormaPagamento();
            return OkRetornoBase(listaformaPagamentoModel);
        }

        [Route("EditarFormaPagamento")]
        [Authorize(Roles = "EditarFormaPagamento")]
        [InvalidateCacheOutput("ObterListaFormaPagamento")]
        [HttpPut]
        public IHttpActionResult EditarFormaPagamento(FormaPagamentoModel model)
        {
            var formaPagamentoModel = _formaPagamentoService.AtualizarFormaPagamento(model);
            return OkRetornoBase(formaPagamentoModel);
        }

        [Route("ExcluirFormaPagamento/{codigoFormaPagamento:int:min(1)}")]
        [Authorize(Roles = "ExcluirFormaPagamento")]
        [InvalidateCacheOutput("ObterListaFormaPagamento")]
        [HttpDelete]
        public IHttpActionResult ExcluirFormaPagamento(int codigoFormaPagamento)
        {
            _formaPagamentoService.ExcluirFormaPagamento(codigoFormaPagamento);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _formaPagamentoService.Dispose();
        }
    }
}
