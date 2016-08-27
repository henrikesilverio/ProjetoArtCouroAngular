using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace ProjetoArtCouro.Api.Security
{
    public class JwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private static byte[] _key = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
        private static string _issuer = ConfigurationManager.AppSettings["issuer"];
        private static string _audience = ConfigurationManager.AppSettings["audience"];

        private readonly OAuthAuthorizationServerOptions _options;

        public JwtFormat(OAuthAuthorizationServerOptions options)
        {
            _options = options;
        }

        public string SignatureAlgorithm => "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

        public string DigestAlgorithm => "http://www.w3.org/2001/04/xmlenc#sha256";

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_options.AccessTokenExpireTimeSpan.TotalMinutes);
            var signingCredentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(_key),
                SignatureAlgorithm,
                DigestAlgorithm);
            var token = new JwtSecurityToken(_issuer, _audience, data.Identity.Claims, now, expires, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}