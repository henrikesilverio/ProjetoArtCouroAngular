using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IEstoque;
using ProjetoArtCouro.Domain.Models.Estoque;

namespace ProjetoArtCouro.Api.Controllers.Estoques
{
    [RoutePrefix("api/Estoque")]
    public class EstoqueController : BaseApiController
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [Route("PesquisarEstoque")]
        [HttpPost]
        public IHttpActionResult PesquisarCompra(PesquisaEstoqueModel model)
        {
            var estoques = _estoqueService.PesquisarEstoque(model);
            return OkRetornoBase(estoques);
        }

        protected override void Dispose(bool disposing)
        {
            _estoqueService.Dispose();
        }
    }
}
