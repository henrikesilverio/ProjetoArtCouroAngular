using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;

namespace ProjetoArtCouro.Api.Security
{
    public class OAuthServerOptions : OAuthAuthorizationServerOptions
    {
        
        public OAuthServerOptions()
        {
            AllowInsecureHttp = true;
            TokenEndpointPath = new PathString("/api/security/token");
            //AuthorizeEndpointPath = new PathString();
            AccessTokenExpireTimeSpan = TimeSpan.FromHours(1);
            Provider = new AuthorizationServerProvider();
            AccessTokenFormat = new JwtFormat(this);
            RefreshTokenProvider = new ApplicationRefreshTokenProvider();
        }
    }
}