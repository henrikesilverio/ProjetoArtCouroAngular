﻿using System;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using ProjetoArtCouro.Api.AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Api.Security;
using ProjetoArtCouro.Domain.Contracts.IService.IAutenticacao;
using ProjetoArtCouro.Startup.DependencyResolver;
using Microsoft.Owin.Security;

namespace ProjetoArtCouro.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //Resolução de depêndencia
            var container = new UnityContainer();
            DependencyResolver.Resolve(container);
            config.DependencyResolver = new UnityResolver(container);

            ConfigureWebApi(config);
            ConfigureOAuth(app, container.Resolve<IAutenticacao>());

            //Deixa o serviço publico sem restrições
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            //Incluir os mapeamento de classe
            AutoMapperConfig.RegisterMappings();
        }

        public void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/id",
                defaults: new { id = RouteParameter.Optional }
                );
        }

        public void ConfigureOAuth(IAppBuilder app, IAutenticacao serviceAutenticacao)
        {
            var issuer = ConfigurationManager.AppSettings["issuer"];
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new AuthorizationServerProvider(serviceAutenticacao),
                AccessTokenFormat = new CustomJwtFormat(issuer)
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                }
            });
        }
    }
}