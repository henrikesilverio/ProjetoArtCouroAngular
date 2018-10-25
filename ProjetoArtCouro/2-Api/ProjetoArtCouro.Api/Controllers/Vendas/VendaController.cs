using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IVenda;
using ProjetoArtCouro.Domain.Models.Venda;

namespace ProjetoArtCouro.Api.Controllers.Vendas
{
    [RoutePrefix("api/Venda")]
    public class VendaController : BaseApiController
    {
        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [Route("CriarVenda")]
        [Authorize(Roles = "NovaVenda")]
        [HttpPost]
        public IHttpActionResult CriarVenda(VendaModel model)
        {
            _vendaService.CriarVenda(CodigoUsuarioLogado, model);
            return OkRetornoBase();
        }

        [Route("PesquisarVenda")]
        [Authorize(Roles = "PesquisaVenda")]
        [HttpPost]
        public IHttpActionResult PesquisarVenda(PesquisaVendaModel model)
        {
            var vendas = _vendaService.PesquisarVenda(CodigoUsuarioLogado, model);
            return OkRetornoBase(vendas);
        }

        [Route("PesquisarVendaPorCodigo/{codigoVenda:int:min(1)}")]
        [Authorize(Roles = "EditarVenda")]
        [HttpGet]
        public IHttpActionResult PesquisarVendaPorCodigo(int codigoVenda)
        {
            var venda = _vendaService.ObterVendaPorCodigo(codigoVenda);
            return OkRetornoBase(venda);
        }

        [Route("EditarVenda")]
        [Authorize(Roles = "EditarVenda")]
        [HttpPut]
        public IHttpActionResult EditarVenda(VendaModel model)
        {
            _vendaService.AtualizarVenda(model);
            return OkRetornoBase();
        }

        [Route("ExcluirVenda/{codigoVenda:int:min(1)}")]
        [Authorize(Roles = "ExcluirVenda")]
        [HttpDelete]
        public IHttpActionResult ExcluirVenda(int codigoVenda)
        {
            _vendaService.ExcluirVenda(codigoVenda);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _vendaService.Dispose();
        }
    }
}
