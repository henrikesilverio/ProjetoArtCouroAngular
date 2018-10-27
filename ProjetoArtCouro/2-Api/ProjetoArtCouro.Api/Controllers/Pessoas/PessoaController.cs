using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pessoas
{
    [RoutePrefix("api/Pessoa")]
    public class PessoaController : BaseApiController
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route("ObterListaEstado")]
        [Authorize(Roles = "NovoCliente, EditarCliente, NovoFornecedor, EditarFornecedor, NovoFuncionario, EditarFuncionario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaEstado()
        {
            var estados = _pessoaService.ObterEstados();
            return OkRetornoBase(estados);
        }

        [Route("ObterListaEstadoCivil")]
        [Authorize(Roles = "NovoCliente, EditarCliente, NovoFornecedor, EditarFornecedor, NovoFuncionario, EditarFuncionario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaEstadoCivil()
        {
            var estadosCivis = _pessoaService.ObterEstadosCivis();
            return OkRetornoBase(estadosCivis);
        }

        [Route("ObterListaPessoa")]
        [Authorize(Roles = "NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaPessoa()
        {
            var pessoas = _pessoaService.ObterListaPessoa();
            return OkRetornoBase(pessoas);
        }

        protected override void Dispose(bool disposing)
        {
            _pessoaService.Dispose();
        }
    }
}
