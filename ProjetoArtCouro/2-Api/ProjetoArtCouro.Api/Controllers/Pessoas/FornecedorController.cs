using System.Collections.Generic;
using System.Web.Http;
using ProjetoArtCouro.Api.Extensions;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Fornecedor;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.Mapping;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pessoas
{
    [RoutePrefix("api/Fornecedor")]
    public class FornecedorController : BaseApiController
    {
        private readonly IPessoaService _pessoaService;

        public FornecedorController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route("CriarFornecedor")]
        [Authorize(Roles = "NovoFornecedor")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFornecedor")]
        [InvalidateCacheOutput("ObterListaFornecedor")]
        [InvalidateCacheOutput("ObterFornecedorPorCodigo")]
        [HttpPost]
        public IHttpActionResult CriarFornecedor(FornecedorModel model)
        {
            _pessoaService.CriarPessoa(model);
            return OkRetornoBase();
        }

        [Route("PesquisarFornecedor")]
        [Authorize(Roles = "PesquisaFornecedor")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpPost]
        public IHttpActionResult PesquisarFornecedor(PesquisaPessoaModel model)
        {
            model.TipoPapelPessoa = TipoPapelPessoaEnum.Fornecedor;
            var pessoas = _pessoaService.PesquisarPessoa(model);
            return OkRetornoBase(pessoas);
        }

        [Route("ObterFornecedorPorCodigo/{codigoFornecedor:int:min(1)}")]
        [Authorize(Roles = "EditarFornecedor")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterFornecedorPorCodigo(int codigoFornecedor)
        {
            var pessoaModel = _pessoaService.ObterPessoaPorCodigo(codigoFornecedor);
            return OkRetornoBase(pessoaModel);
        }

        [Route("ObterListaFornecedor")]
        [Authorize(Roles = "NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaFornecedor()
        {
            var pessoas = _pessoaService.ObterListaPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum.Fornecedor);
            return OkRetornoBase(Map<List<FornecedorModel>>.MapperTo(pessoas));
        }

        [Route("EditarFornecedor")]
        [Authorize(Roles = "EditarFornecedor")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFornecedor")]
        [InvalidateCacheOutput("ObterListaFornecedor")]
        [InvalidateCacheOutput("ObterFornecedorPorCodigo")]
        [HttpPut]
        public IHttpActionResult EditarFornecedor(FornecedorModel model)
        {
            _pessoaService.AtualizarPessoa(model);
            return OkRetornoBase();
        }

        [Route("ExcluirFornecedor/{codigoFornecedor:int:min(1)}")]
        [Authorize(Roles = "ExcluirFornecedor")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFornecedor")]
        [InvalidateCacheOutput("ObterListaFornecedor")]
        [InvalidateCacheOutput("ObterFornecedorPorCodigo")]
        [HttpDelete]
        public IHttpActionResult ExcluirFornecedor(int codigoFornecedor)
        {
            _pessoaService.ExcluirPessoa(codigoFornecedor);
            return OkRetornoBase();
        }

        protected override void Dispose(bool disposing)
        {
            _pessoaService.Dispose();
        }
    }
}
