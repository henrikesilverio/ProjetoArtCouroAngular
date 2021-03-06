﻿using BoDi;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Api;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Test.API.Infra;
using System.Collections;
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
        private ServiceRequest _serviceRequest;

        public Initialization(IObjectContainer objectContainer,
            ScenarioContext scenatriContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenatriContext;
            _scenarioContext.Add("Conteudo", null);
            _scenarioContext.Add("Response", null);
            _scenarioContext.Add("TestShared", null);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var server = TestServer.Create<Startup>();
            var context = new DataBaseContext();
            _serviceRequest = new ServiceRequest(server);

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

        [When(@"realizar uma chamada Get ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaGetAoEndereco(string endereco)
        {
            var response = _serviceRequest.Get(endereco);
            _scenarioContext["Response"] = response;
        }

        [When(@"realizar uma chamada Post ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaPostAoEndereco(string endereco)
        {
            var response = _serviceRequest.Post(_scenarioContext["Conteudo"], endereco);
            _scenarioContext["Response"] = response;
        }

        [When(@"realizar uma chamada Put ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaPutAoEndereco(string endereco)
        {
            var response = _serviceRequest.Put(_scenarioContext["Conteudo"], endereco);
            _scenarioContext["Response"] = response;
        }

        [When(@"realizar uma chamada Delete ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaDeleteAoEndereco(string endereco)
        {
            var response = _serviceRequest.Delete(endereco);
            _scenarioContext["Response"] = response;
        }

        [Then(@"retorne sucesso")]
        public void EntaoRetorneSucesso()
        {
            var testShared = _scenarioContext["TestShared"] as ITestShared;
            Assert.AreNotEqual(testShared, null, "Teste compartilhado não implementado");
            _scenarioContext.Remove("RetornoBase");
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

        [Then(@"que o retorno tenha uma lista com '(.*)' itens")]
        public void EntaoQueORetornoTenhaUmaListaComItens(int count)
        {
            var retornoBase = _scenarioContext["RetornoBase"] as RetornoBase<object>;
            Assert.IsNotNull(retornoBase, "RetornoBase está nulo");

            var objetoRetorno = retornoBase.ObjetoRetorno as IList;
            Assert.IsNotNull(objetoRetorno, "ObjetoRetorno está nulo");
            Assert.AreEqual(objetoRetorno.Count, count, "Quantidade de invalida");
        }

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