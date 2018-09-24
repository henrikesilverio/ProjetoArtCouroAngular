using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Test.API.Infra;
using System.Net;
using TechTalk.SpecFlow;
using System.Net.Http;

namespace ProjetoArtCouro.Test.API.Controllers.Pessoas
{
    public class ClienteTestShared : ITestShared
    {
        private readonly ScenarioContext _scenarioContext;

        public ClienteTestShared(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public void ClearData()
        {
            throw new System.NotImplementedException();
        }

        public void ValidateReturnSuccess()
        {
            var response = (HttpResponseMessage)_scenarioContext["Response"];
            Assert.AreNotEqual(null, response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var returnBase = response.Content.ReadAsAsync<RetornoBase<object>>().Result;
            _scenarioContext.Add("ReturnBase", returnBase);
        }
    }
}
