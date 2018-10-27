using System.Collections.Generic;
using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IVenda;
using ProjetoArtCouro.Domain.Models.ContaReceber;

namespace ProjetoArtCouro.Api.Controllers.ContasReceber
{
    [RoutePrefix("api/ContaReceber")]
    public class ContaReceberController : BaseApiController
    {
        private readonly IContaReceberService _contaReceberService;

        public ContaReceberController(IContaReceberService contaReceberService)
        {
            _contaReceberService = contaReceberService;
        }

        [Route("PesquisaContaReceber")]
        [Authorize(Roles = "PesquisaContaReceber")]
        [HttpPost]
        public IHttpActionResult PesquisaContaReceber(PesquisaContaReceberModel model)
        {
            var contasReceber = _contaReceberService.PesquisarContaReceber(CodigoUsuarioLogado, model);
            return OkRetornoBase(contasReceber);
        }

        [Route("ReceberConta")]
        [Authorize(Roles = "PesquisaContaReceber")]
        [HttpPut]
        public IHttpActionResult ReceberConta(List<ContaReceberModel> model)
        {
            _contaReceberService.ReceberContas(model);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _contaReceberService.Dispose();
        }
    }
}
