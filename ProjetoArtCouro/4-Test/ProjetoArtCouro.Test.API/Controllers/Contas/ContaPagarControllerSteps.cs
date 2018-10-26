using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.ContaPagar;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Contas
{
    [Binding]
    public class ContaPagarControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ContaPagarControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new ContaTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do filtro de pesquisa de contas a pagar com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeContasAPagarComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaContaPagarModel>();
        }

        [Given(@"que preencha os dados da contas a pagar com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDaContasAPagarComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateSet<ContaPagarModel>();
        }
    }
}
