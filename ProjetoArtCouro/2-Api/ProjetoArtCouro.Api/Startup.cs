using Microsoft.Owin.Cors;
using Microsoft.Practices.Unity;
using Owin;
using ProjetoArtCouro.Api.AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Api.Security;
using ProjetoArtCouro.Domain.Contracts.IService.IAutenticacao;
using ProjetoArtCouro.Startup.DependencyResolver;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            var enableCorsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(enableCorsAttribute);

            ConfigureWebApi(config);
            ConfigureOAuth(app, config);

            //Deixa o serviço publico sem restrições
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            //Configuração da serialização json
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

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

        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            var autenticacaoService = (IAutenticacao)config.DependencyResolver.GetService(typeof(IAutenticacao));
            app.UseOAuthAuthorizationServer(new OAuthServerOptions(autenticacaoService));
            app.UseJwtBearerAuthentication(new JwtOptions());
        }
    }
}