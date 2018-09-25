using BoDi;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Api;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Test.API.Infra;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace ProjetoArtCouro.Test.API
{
    [Binding]
    public class Initialization
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        public Initialization(IObjectContainer objectContainer,
            ScenarioContext scenatriContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenatriContext;
            _scenarioContext.Add("TestShared", null);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var server = TestServer.Create<Startup>();
            var context = new DataBaseContext();

            _objectContainer.RegisterInstanceAs(server, typeof(TestServer));
            _objectContainer.RegisterInstanceAs(context, typeof(DataBaseContext), null, true);
            _objectContainer.RegisterTypeAs<ServiceRequest, ServiceRequest>();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var testShared = _scenarioContext["TestShared"] as ITestShared;
            Assert.AreNotEqual(testShared, null, "Teste compartilhado não implementado");
            testShared.ClearData();
        }

        [Then(@"retorne sucesso")]
        public void EntaoRetorneSucesso()
        {
            var testShared = _scenarioContext["TestShared"] as ITestShared;
            Assert.AreNotEqual(testShared, null, "Teste compartilhado não implementado");
            testShared.ValidateReturnSuccess();
        }

        [Then(@"retorne erro")]
        public void EntaoRetorneErro()
        {
            ErrorReturn(HttpStatusCode.BadRequest);
        }

        [Then(@"retorne erro HttpStatusCode (.*)")]
        public void EntaoRetorneErroHttpStatusCode(int statusCode)
        {
            ErrorReturn((HttpStatusCode)statusCode);
        }

        //[Then(@"que o retorno tenha uma lista com '(.*)' itens")]
        //public void EntaoQueORetornoTenhaUmaListaComItens(int count)
        //{
        //    var returnBase = _scenarioContext["ReturnBase"] as ReturnBase<object>;
        //    Assert.IsNotNull(returnBase, "ReturnBase está nulo");

        //    var objectReturn = returnBase.ObjectReturn as IList;
        //    Assert.IsNotNull(objectReturn, "ObjectReturn está nulo");
        //    Assert.AreEqual(objectReturn.Count, count, "Quantidade de invalida");
        //}

        private void ErrorReturn(HttpStatusCode statusCode)
        {
            var response = (HttpResponseMessage)_scenarioContext["Response"];
            Assert.AreNotEqual(null, response.Content);
            Assert.AreEqual(statusCode, response.StatusCode);

            //if (statusCode != HttpStatusCode.NotFound)
            //{
            //    var errorBase = response.Content.ReadAsAsync<ErrorBase>().Result;
            //    Assert.AreNotEqual(string.Empty, errorBase.Message);
            //}
        }
    }
}