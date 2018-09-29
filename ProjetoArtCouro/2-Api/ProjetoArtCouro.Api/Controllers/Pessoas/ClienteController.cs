using ProjetoArtCouro.Api.Extensions;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Cliente;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pessoas
{
    [RoutePrefix("api/Cliente")]
    public class ClienteController : BaseApiController
    {
        private readonly IPessoaService _pessoaService;

        public ClienteController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route("CriarCliente")]
        [Authorize(Roles = "NovoCliente")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarCliente")]
        [InvalidateCacheOutput("ObterClientePorCodigo")]
        [HttpPost]
        public IHttpActionResult CriarCliente(ClienteModel model)
        {
            _pessoaService.CriarPessoa(model);
            return OkRetornoBase();
        }

        [Route("PesquisarCliente")]
        [Authorize(Roles = "PesquisaCliente")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpPost]
        public IHttpActionResult PesquisarCliente(PesquisaPessoaModel model)
        {
            model.TipoPapelPessoa = TipoPapelPessoaEnum.Cliente;
            var pessoas = _pessoaService.PesquisarPessoa(model);
            return OkRetornoBase(pessoas);
        }

        [Route("ObterClientePorCodigo/{codigoCliente:int:min(1)}")]
        [Authorize(Roles = "EditarCliente")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterClientePorCodigo(int codigoCliente)
        {
            var pessoaModel = _pessoaService.ObterPessoaPorCodigo(codigoCliente);
            return OkRetornoBase(pessoaModel);
        }

        [Route("EditarCliente")]
        [Authorize(Roles = "EditarCliente")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarCliente")]
        [InvalidateCacheOutput("ObterClientePorCodigo")]
        [HttpPut]
        public IHttpActionResult EditarCliente(ClienteModel model)
        {
            _pessoaService.AtualizarPessoa(model);
            return OkRetornoBase();
        }

        [Route("ExcluirCliente/{codigoCliente:int:min(1)}")]
        [Authorize(Roles = "ExcluirCliente")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarCliente")]
        [InvalidateCacheOutput("ObterClientePorCodigo")]
        [HttpDelete]
        public IHttpActionResult ExcluirCliente(int codigoCliente)
        {
            _pessoaService.ExcluirPessoa(codigoCliente);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _pessoaService.Dispose();
        }
    }
}