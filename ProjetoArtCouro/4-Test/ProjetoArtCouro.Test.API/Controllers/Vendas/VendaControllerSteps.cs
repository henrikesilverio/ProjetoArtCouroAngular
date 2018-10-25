using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Venda;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Vendas
{
    [Binding]
    public class VendaControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public VendaControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new VendaTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados da venda com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDaVendaComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<VendaModel>();
        }

        [Given(@"que preecha os dados do item de venda com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDoItemDeVendaComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o VendaModel antes");
            var itensVendaModel = (List<ItemVendaModel>)table.CreateSet<ItemVendaModel>();
            ((VendaModel)_scenarioContext["Conteudo"]).ItemVendaModel = itensVendaModel;
        }

        [Given(@"que preencha os dados do filtro de pesquisa de venda com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeVendaComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaVendaModel>();
        }
    }
}
