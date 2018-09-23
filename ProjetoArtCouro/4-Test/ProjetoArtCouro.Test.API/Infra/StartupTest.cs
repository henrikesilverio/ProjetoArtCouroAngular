using Owin;
using System.Web.Http;

namespace ProjetoArtCouro.Test.API.Infra
{
    internal class StartupTest
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //// Configure SimpleInjector
            //IoCConfig.Initialize(config);

            //// Configure Auth0
            //AuthZeroConfig.ConfigureAuthZero(app);

            //// Configure Web API
            //WebApiConfig.Register(config);

            //// RegisterMappings
            //AutoMapperConfig.RegisterMappings();

            //// Register Filters
            //FilterConfig.Register(config);

            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //app.UseWebApi(config);
        }
    }
}
