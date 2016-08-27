using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using System.Configuration;

namespace ProjetoArtCouro.Api.Security
{
    public class JwtOptions : JwtBearerAuthenticationOptions
    {
        private static byte[] _key = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
        private static string _issuer = ConfigurationManager.AppSettings["issuer"];
        private static string _audience = ConfigurationManager.AppSettings["audience"];

        public JwtOptions()
        {
            AllowedAudiences = new[] { _audience };
            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(_issuer, _key)
            };
        }
    }
}