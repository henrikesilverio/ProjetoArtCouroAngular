using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Test.API.Infra;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace ProjetoArtCouro.Test.API.Controllers.Pagamentos
{
    public class CondicaoPagamentoTestShared : ITestShared
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly DataBaseContext _context;

        public CondicaoPagamentoTestShared(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _context = context;
        }

        public void ClearData()
        {
            var commands = new[]
            {
                "DELETE FROM [CondicaoPagamento] DBCC CHECKIDENT('CondicaoPagamento', RESEED, 0)"
            };

            foreach (var command in commands)
            {
                _context.Database.ExecuteSqlCommand(command);
            }
        }

        public void ValidateReturnSuccess()
        {
            var response = (HttpResponseMessage)_scenarioContext["Response"];
            Assert.AreNotEqual(null, response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var retornoBase = response.Content.ReadAsAsync<RetornoBase<object>>().Result;
            _scenarioContext.Add("RetornoBase", retornoBase);
        }
    }
}
