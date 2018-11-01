using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Api.Helpers
{
    public class BaseApiController : ApiController
    {
        public int CodigoUsuarioLogado
        {
            get
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var usuarioCodigo = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                    .Select(c => c.Value).SingleOrDefault();
                int.TryParse(usuarioCodigo, out int usuarioCodigoParce);
                return usuarioCodigoParce;
            }
        }

        public HttpResponseMessage ReturnError(Exception ex)
        {
            var retornoBase = new RetornoBase<ExceptionModel>
            {
                ObjetoRetorno = Map<ExceptionModel>.MapperTo(ex),
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