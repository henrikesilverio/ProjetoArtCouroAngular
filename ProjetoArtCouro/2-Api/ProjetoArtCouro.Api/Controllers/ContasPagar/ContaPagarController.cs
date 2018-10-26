using System.Collections.Generic;
using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.ICompra;
using ProjetoArtCouro.Domain.Models.ContaPagar;

namespace ProjetoArtCouro.Api.Controllers.ContasPagar
{
    [RoutePrefix("api/ContaPagar")]
    public class ContaPagarController : BaseApiController
    {
        private readonly IContaPagarService _contaPagarService;

        public ContaPagarController(IContaPagarService contaPagarService)
        {
            _contaPagarService = contaPagarService;
        }

        [Route("PesquisaContaPagar")]
        [Authorize(Roles = "PesquisaContaPagar")]
        [HttpPost]
        public IHttpActionResult PesquisaContaPagar(PesquisaContaPagarModel model)
        {
            var contasPagar = _contaPagarService.PesquisarContaPagar(CodigoUsuarioLogado, model);
            return OkRetornoBase(contasPagar);
        }

        [Route("PagarConta")]
        [Authorize(Roles = "PesquisaContaPagar")]
        [HttpPut]
        public IHttpActionResult PagarConta(List<ContaPagarModel> model)
        {
            _contaPagarService.PagarContas(model);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _contaPagarService.Dispose();
        }
    }
}
