using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.ICompra;
using ProjetoArtCouro.Domain.Entities.Compras;
using ProjetoArtCouro.Domain.Entities.Usuarios;
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
            var usuarioCodigo = ObterCodigoUsuarioLogado();
            _compraService.CriarCompra(usuarioCodigo, model);
            return OkRetornoBase();
        }

        [Route("PesquisarCompra")]
        [Authorize(Roles = "PesquisaCompra")]
        [HttpPost]
        public IHttpActionResult PesquisarCompra(PesquisaCompraModel model)
        {
            var usuarioCodigo = ObterCodigoUsuarioLogado();
            var compras = _compraService.PesquisarCompra(usuarioCodigo, model);
            return OkRetornoBase(compras);
        }

        [Route("PesquisarCompraPorCodigo/{codigoCompra:int:min(1)}")]
        [Authorize(Roles = "EditarCompra")]
        [HttpPost]
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

        private static int ObterCodigoUsuarioLogado()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var usuarioCodigo = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                .Select(c => c.Value).SingleOrDefault();
            return usuarioCodigo.ToInt();
        }

        protected override void Dispose(bool disposing)
        {
            _compraService.Dispose();
        }
    }
}
