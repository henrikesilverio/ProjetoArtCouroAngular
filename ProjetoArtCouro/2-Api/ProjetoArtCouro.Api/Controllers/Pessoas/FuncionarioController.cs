using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ProjetoArtCouro.Api.Extensions;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Funcionario;
using ProjetoArtCouro.Domain.Models.Pessoa;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pessoas
{
    [RoutePrefix("api/Funcionario")]
    public class FuncionarioController : BaseApiController
    {
        private readonly IPessoaService _pessoaService;
        public FuncionarioController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route("CriarFuncionario")]
        [Authorize(Roles = "NovoFuncionario")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFuncionario")]
        [InvalidateCacheOutput("ObterFuncionarioPorCodigo")]
        [HttpPost]
        public IHttpActionResult CriarFuncionario(FuncionarioModel model)
        {
            try
            {
                _pessoaService.CriarPessoa(model);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("PesquisarFuncionario")]
        [Authorize(Roles = "PesquisaFuncionario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpPost]
        public IHttpActionResult PesquisarFuncionario(PesquisaPessoaModel model)
        {
            model.TipoPapelPessoa = TipoPapelPessoaEnum.Funcionario;
            var pessoas = _pessoaService.PesquisarPessoa(model);
            return OkRetornoBase(pessoas);
        }

        [Route("ObterFuncionarioPorCodigo/{codigoFuncionario:int:min(1)}")]
        [Authorize(Roles = "EditarFuncionario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterFuncionarioPorCodigo(int codigoFuncionario)
        {
            var pessoaModel = _pessoaService.ObterPessoaPorCodigo(codigoFuncionario);
            return OkRetornoBase(pessoaModel);
        }

        [Route("EditarFuncionario")]
        [Authorize(Roles = "EditarFuncionario")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFuncionario")]
        [InvalidateCacheOutput("ObterFuncionarioPorCodigo")]
        [HttpPut]
        public IHttpActionResult EditarFuncionario(FuncionarioModel model)
        {
            try
            {
                _pessoaService.AtualizarPessoa(model);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ExcluirFuncionario/{codigoFuncionario:int:min(1)}")]
        [Authorize(Roles = "ExcluirFuncionario")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [InvalidateCacheOutput("PesquisarFuncionario")]
        [InvalidateCacheOutput("ObterFuncionarioPorCodigo")]
        [HttpDelete]
        public IHttpActionResult ExcluirFuncionario(int codigoFuncionario)
        {
            try
            {
                _pessoaService.ExcluirPessoa(codigoFuncionario);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _pessoaService.Dispose();
        }
    }
}
