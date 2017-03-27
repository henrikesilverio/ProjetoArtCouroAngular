using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using ProjetoArtCouro.Api.Extensions;
using ProjetoArtCouro.Domain.Models.Common;

namespace ProjetoArtCouro.Api.Helpers
{
    [DeflateCompression]
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage ReturnError(Exception ex)
        {
            var retornoBase = new RetornoBase<ExceptionModel>
            {
                ObjetoRetorno = Mapper.Map<ExceptionModel>(ex),
                Mensagem = ex.Message,
                TemErros = true
            };
            return Request.CreateResponse(HttpStatusCode.BadRequest, retornoBase);
        }

        public HttpResponseMessage ReturnSuccess()
        {
            var retornoBase = new RetornoBase<object>();
            return Request.CreateResponse(HttpStatusCode.OK, retornoBase);
        }

        public HttpResponseMessage ReturnSuccess<T>(T objetReturn)
        {
            var retornoBase = new RetornoBase<T>
            {
                ObjetoRetorno = objetReturn
            };
            var response = Request.CreateResponse(HttpStatusCode.OK, retornoBase);
            return response;
        }

        public OkNegotiatedContentResult<RetornoBase<T>> OkRetornoBase<T>(T retorno)
        {
            var retornoBase = new RetornoBase<T>
            {
                ObjetoRetorno = retorno
            };
            return Ok(retornoBase);
        }

        public OkNegotiatedContentResult<RetornoBase<object>> OkRetornoBase()
        {
            var retornoBase = new RetornoBase<object>();
            return Ok(retornoBase);
        }
    }
}