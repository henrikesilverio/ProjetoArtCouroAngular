using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Api.Controllers.Pessoas;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Cliente;
using ProjetoArtCouro.Domain.Models.Common;
using TechTalk.SpecFlow;

namespace ProjetoArtCouro.Test.API.Controllers.Pessoas
{
    [Binding]
    public class ClienteControllerSpec
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPessoaService _pessoaService;

        public ClienteControllerSpec(ScenarioContext scenarioContext, IPessoaService pessoaService)
        {
            _scenarioContext = scenarioContext;
            _pessoaService = pessoaService;
        }

        [Given(@"que tenha um cliente sem preencher todas as informações")]
        public void DadoQueTenhaUmClienteSemPreencherTodasAsInformacoes()
        {
            _scenarioContext.Add("ClienteModel", new ClienteModel());
        }

        [When(@"chamar o metodo CriarCliente")]
        public void QuandoChamarOMetodoCriarCliente()
        {
            var controller = new ClienteController(_pessoaService)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var clienteModel = _scenarioContext["ClienteModel"] as ClienteModel;
            var response = controller.CriarCliente(clienteModel);
            var http = response.ExecuteAsync(CancellationToken.None);
            _scenarioContext.Add("StatusCode", http.Result.StatusCode);
            _scenarioContext.Add("Content", http.Result.Content);
        }

        [Then(@"retorno um erro")]
        public void EntaoRetornoUmErro()
        {
            var statusCode = (HttpStatusCode)_scenarioContext["StatusCode"];
            Assert.AreEqual(statusCode, HttpStatusCode.InternalServerError);
            var content = _scenarioContext["Content"] as HttpContent;
            Assert.IsNotNull(content);
            var erroBase = content.ReadAsAsync<ErroBase>().Result;
            Assert.AreNotEqual(erroBase.Message, string.Empty);
        }
    }
}
