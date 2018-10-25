using System.Linq;
using System.Security.Claims;
using System.Threading;
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
            var usuarioCodigo = ObterCodigoUsuarioLogado();
            _vendaService.CriarVenda(usuarioCodigo, model);
            return OkRetornoBase();
        }

        [Route("PesquisarVenda")]
        [Authorize(Roles = "PesquisaVenda")]
        [HttpPost]
        public IHttpActionResult PesquisarVenda(PesquisaVendaModel model)
        {
            var usuarioCodigo = ObterCodigoUsuarioLogado();
            var vendas = _vendaService.PesquisarVenda(usuarioCodigo, model);
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

        private static int ObterCodigoUsuarioLogado()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var usuarioCodigo = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                .Select(c => c.Value).SingleOrDefault();
            return usuarioCodigo.ToInt();
        }

        protected override void Dispose(bool disposing)
        {
            _vendaService.Dispose();
        }
    }
}
