using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Test.API.Infra;
using System.Net;
using TechTalk.SpecFlow;
using System.Net.Http;
using ProjetoArtCouro.DataBase.DataBase;

namespace ProjetoArtCouro.Test.API.Controllers.Pessoas
{
    public class ClienteTestShared : ITestShared
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly DataBaseContext _context;

        public ClienteTestShared(
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
                "DELETE FROM [MeioComunicacao] DBCC CHECKIDENT('MeioComunicacao', RESEED, 0)",
                "DELETE FROM [Endereco] DBCC CHECKIDENT('Endereco', RESEED, 0)",
                "DELETE FROM [PessoaFisica] DBCC CHECKIDENT('PessoaFisica', RESEED, 0)",
                "DELETE FROM [PessoaJuridica] DBCC CHECKIDENT('PessoaFisica', RESEED, 0)",
                "DELETE FROM [PessoaPapel]",
                "DELETE FROM [Pessoa] DBCC CHECKIDENT('Pessoa', RESEED, 0)"
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

            var returnBase = response.Content.ReadAsAsync<RetornoBase<object>>().Result;
            _scenarioContext.Add("ReturnBase", returnBase);
        }
    }
}
