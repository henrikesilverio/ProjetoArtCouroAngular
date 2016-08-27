using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using ProjetoArtCouro.Domain.Contracts.IService.IAutenticacao;

namespace ProjetoArtCouro.Api.Security
{
    public class OAuthServerOptions : OAuthAuthorizationServerOptions
    {
        
        public OAuthServerOptions(IAutenticacao autenticacaoService)
        {
            AllowInsecureHttp = true;
            TokenEndpointPath = new PathString("/api/security/token");
            AccessTokenExpireTimeSpan = TimeSpan.FromHours(2);
            Provider = new AuthorizationServerProvider(autenticacaoService);
            AccessTokenFormat = new JwtFormat(this);
        }
    }
}