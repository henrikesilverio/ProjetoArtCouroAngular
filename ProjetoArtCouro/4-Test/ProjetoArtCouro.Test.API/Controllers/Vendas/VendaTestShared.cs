﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Test.API.Infra;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace ProjetoArtCouro.Test.API.Controllers.Vendas
{
    public class VendaTestShared : ITestShared
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly DataBaseContext _context;

        public VendaTestShared(
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
                "DELETE FROM [Estoque] DBCC CHECKIDENT('Estoque', RESEED, 0)",
                "DELETE FROM [Venda] DBCC CHECKIDENT('Venda', RESEED, 0)",
                "DELETE FROM [ItemVenda] DBCC CHECKIDENT('ItemVenda', RESEED, 0)",
                "DELETE FROM [Compra] DBCC CHECKIDENT('Compra', RESEED, 0)",
                "DELETE FROM [ItemCompra] DBCC CHECKIDENT('ItemCompra', RESEED, 0)",
                "DELETE FROM [MeioComunicacao] DBCC CHECKIDENT('MeioComunicacao', RESEED, 0)",
                "DELETE FROM [Endereco] DBCC CHECKIDENT('Endereco', RESEED, 0)",
                "DELETE FROM [PessoaFisica] DBCC CHECKIDENT('PessoaFisica', RESEED, 0)",
                "DELETE FROM [PessoaJuridica] DBCC CHECKIDENT('PessoaFisica', RESEED, 0)",
                "DELETE FROM [PessoaPapel]",
                "DELETE FROM [Pessoa] DBCC CHECKIDENT('Pessoa', RESEED, 0)",
                "DELETE FROM [CondicaoPagamento] DBCC CHECKIDENT('CondicaoPagamento', RESEED, 0)",
                "DELETE FROM [FormaPagamento] DBCC CHECKIDENT('FormaPagamento', RESEED, 0)",
                "DELETE FROM [Produto] DBCC CHECKIDENT('Produto', RESEED, 0)",
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
