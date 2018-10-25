using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Compra;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Compras
{
    [Binding]
    public class CompraControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CompraControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new CompraTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados da compra com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDaCompraComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<CompraModel>();
        }

        [Given(@"que preecha os dados do item de compra com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDoItemDeCompraComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o CompraModel antes");
            var itensCompraModel = (List<ItemCompraModel>)table.CreateSet<ItemCompraModel>();
            ((CompraModel)_scenarioContext["Conteudo"]).ItemCompraModel = itensCompraModel;
        }

        [Given(@"que preencha os dados do filtro de pesquisa de compra com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeCompraComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaCompraModel>();
        }
    }
}
