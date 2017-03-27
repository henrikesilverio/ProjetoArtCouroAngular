using AutoMapper;
using Newtonsoft.Json.Linq;
using ProjetoArtCouro.Api.Extensions;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Cliente;
using ProjetoArtCouro.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

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
        [HttpPost]
        public IHttpActionResult CriarCliente(ClienteModel model)
        {
            try
            {
                var pessoa = Mapper.Map<Pessoa>(model);
                pessoa.Papeis = new List<Papel> { new Papel { PapelCodigo = model.PapelPessoa } };
                //Remove informações que não vão ser gravadas.
                ((List<MeioComunicacao>)pessoa.MeiosComunicacao).RemoveAll(
                    x => string.IsNullOrEmpty(x.MeioComunicacaoNome) && x.MeioComunicacaoCodigo.Equals(0));

                if (model.EPessoaFisica)
                {
                    pessoa.PessoaFisica = Mapper.Map<PessoaFisica>(model);
                    _pessoaService.CriarPessoaFisica(pessoa);
                }
                else
                {
                    pessoa.PessoaJuridica = Mapper.Map<PessoaJuridica>(model);
                    _pessoaService.CriarPessoaJuridica(pessoa);
                }
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("PesquisarCliente")]
        [Authorize(Roles = "PesquisaCliente")]
        [HttpPost]
        public IHttpActionResult PesquisarCliente(PesquisaClienteModel model)
        {
            try
            {
                if (model.EPessoaFisica)
                {
                    var listaPessoaFisica = _pessoaService.PesquisarPessoaFisica(model.CodigoCliente ?? 0, model.Nome,
                    model.CPFCNPJ, model.Email, TipoPapelPessoaEnum.Cliente);
                    return OkRetornoBase(Mapper.Map<List<ClienteModel>>(listaPessoaFisica));
                }
                var listaPessoaJuridica = _pessoaService.PesquisarPessoaJuridica(model.CodigoCliente ?? 0, model.Nome,
                model.CPFCNPJ, model.Email, TipoPapelPessoaEnum.Cliente);
                return OkRetornoBase(Mapper.Map<List<ClienteModel>>(listaPessoaJuridica));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("PesquisarClientePorCodigo")]
        [Authorize(Roles = "EditarCliente")]
        [HttpPost]
        public IHttpActionResult PesquisarClientePorCodigo([FromBody]JObject jObject)
        {
            var codigoCliente = jObject["codigoCliente"].ToObject<int>();
            try
            {
                var pessoa = _pessoaService.ObterPessoaPorCodigo(codigoCliente);
                return OkRetornoBase(pessoa.PessoaFisica != null
                    ? Mapper.Map<ClienteModel>(pessoa.PessoaFisica)
                    : Mapper.Map<ClienteModel>(pessoa.PessoaJuridica));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("EditarCliente")]
        [Authorize(Roles = "EditarCliente")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [HttpPut]
        public IHttpActionResult EditarCliente(ClienteModel model)
        {
            try
            {
                var pessoa = Mapper.Map<Pessoa>(model);
                //Remove informações que não vão ser gravadas.
                ((List<MeioComunicacao>)pessoa.MeiosComunicacao).RemoveAll(
                    x => string.IsNullOrEmpty(x.MeioComunicacaoNome) && x.MeioComunicacaoCodigo.Equals(0));
                if (model.EPessoaFisica)
                {
                    pessoa.PessoaFisica = Mapper.Map<PessoaFisica>(model);
                }
                else
                {
                    pessoa.PessoaJuridica = Mapper.Map<PessoaJuridica>(model);
                }
                _pessoaService.AtualizarPessoa(pessoa);
                return OkRetornoBase();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("ExcluirCliente/{codigoCliente:int:min(1)}")]
        [Authorize(Roles = "ExcluirCliente")]
        [InvalidateCacheOutputCustom("ObterListaPessoa", "PessoaController")]
        [HttpDelete]
        public IHttpActionResult ExcluirCliente(int codigoCliente)
        {
            try
            {
                _pessoaService.ExcluirPessoa(codigoCliente);
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