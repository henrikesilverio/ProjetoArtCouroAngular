using ProjetoArtCouro.Domain.Contracts.IException;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ProjetoArtCouro.Api.Helpers
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            var request = filterContext.Request;
            HttpStatusCode httpStatusCode;

            var exception = filterContext.Exception;
            var typeException = exception.GetType();

            if (typeof(IBusinessException).IsAssignableFrom(typeException) ||
                typeof(IDomainException).IsAssignableFrom(typeException))
            {
                httpStatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                httpStatusCode = HttpStatusCode.InternalServerError;
            }

            filterContext.Response = request.CreateErrorResponse(httpStatusCode, exception);
        }
    }
}