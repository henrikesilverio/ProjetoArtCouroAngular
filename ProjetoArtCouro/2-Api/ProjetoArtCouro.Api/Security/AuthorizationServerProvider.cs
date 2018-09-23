using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using ProjetoArtCouro.Domain.Contracts.IService.IAutenticacao;
using ProjetoArtCouro.Resources.Resources;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Web.Http;

namespace ProjetoArtCouro.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private IAutenticacao _autenticacaoService;

        public AuthorizationServerProvider()
        {
            _autenticacaoService = (IAutenticacao)GlobalConfiguration
                .Configuration.DependencyResolver.GetService(typeof(IAutenticacao));
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var user = _autenticacaoService.AutenticarUsuario(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", Erros.InvalidCredentials);
                    return;
                }

                var identity = new ClaimsIdentity("otc");

                identity.AddClaim(new Claim(ClaimTypes.Name, user.UsuarioNome));
                identity.AddClaim(new Claim(ClaimTypes.Sid, user.UsuarioCodigo.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.PrimarySid, user.UsuarioId.ToString()));

                //Setando as permissao do usuario
                var permissoes = _autenticacaoService.ObterPermissoes(user.UsuarioNome);
                foreach (var permissao in permissoes)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, permissao.AcaoNome));
                }

                var principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (Exception)
            {
                context.SetError("invalid_grant", Erros.InvalidCredentials);
            }
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            // Change authentication ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
        }
    }
}