using Owin;
using ProjetoArtCouro.Api.Security;
using ProjetoArtCouro.Startup.DependencyResolver;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjetoArtCouro.Api.Helpers;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using ProjetoArtCouro.Mapping.Configs;

namespace ProjetoArtCouro.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureDependencyResolver(config);

            ConfigureWebApi(config);

            ConfigureFilter(config);

            ConfigureCompressionHandler(config);

            ConfigureOAuth(app);

            app.UseWebApi(config);
            
            MapperConfig.RegisterMappings();
        }

        public void ConfigureDependencyResolver(HttpConfiguration config)
        {
            config.DependencyResolver = DependencyResolver.Resolve();
            GlobalConfiguration.Configuration.DependencyResolver = config.DependencyResolver;
        }

        public void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var enableCorsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(enableCorsAttribute);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/id",
                defaults: new { id = RouteParameter.Optional });

            //Configuração da serialização json
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthServerOptions());
            app.UseJwtBearerAuthentication(new JwtOptions());
        }

        public void ConfigureFilter(HttpConfiguration config)
        {
            config.Filters.Add(new ExceptionFilter());
        }

        public static void ConfigureCompressionHandler(HttpConfiguration config)
        {
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }
    }
}