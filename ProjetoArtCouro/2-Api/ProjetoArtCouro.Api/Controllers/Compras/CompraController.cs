using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.ICompra;
using ProjetoArtCouro.Domain.Models.Compra;
using System.Web.Http;

namespace ProjetoArtCouro.Api.Controllers.Compras
{
    [RoutePrefix("api/Compra")]
    public class CompraController : BaseApiController
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [Route("CriarCompra")]
        [Authorize(Roles = "NovaCompra")]
        [HttpPost]
        public IHttpActionResult CriarCompra(CompraModel model)
        {
            _compraService.CriarCompra(CodigoUsuarioLogado, model);
            return OkRetornoBase();
        }

        [Route("PesquisarCompra")]
        [Authorize(Roles = "PesquisaCompra")]
        [HttpPost]
        public IHttpActionResult PesquisarCompra(PesquisaCompraModel model)
        {
            var compras = _compraService.PesquisarCompra(CodigoUsuarioLogado, model);
            return OkRetornoBase(compras);
        }

        [Route("PesquisarCompraPorCodigo/{codigoCompra:int:min(1)}")]
        [Authorize(Roles = "EditarCompra")]
        [HttpGet]
        public IHttpActionResult PesquisarCompraPorCodigo(int codigoCompra)
        {
            var compra = _compraService.ObterCompraPorCodigo(codigoCompra);
            return OkRetornoBase(compra);
        }

        [Route("EditarCompra")]
        [Authorize(Roles = "EditarCompra")]
        [HttpPut]
        public IHttpActionResult EditarCompra(CompraModel model)
        {
            _compraService.AtualizarCompra(model);
            return OkRetornoBase();
        }

        [Route("ExcluirCompra/{codigoCompra:int:min(1)}")]
        [Authorize(Roles = "ExcluirCompra")]
        [HttpDelete]
        public IHttpActionResult ExcluirCompra(int codigoCompra)
        {
            _compraService.ExcluirCompra(codigoCompra);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _compraService.Dispose();
        }
    }
}
