using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Estoque;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Estoque
{
    [Binding]
    public class EstoqueControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public EstoqueControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new EstoqueTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do filtro de pesquisa de estoque com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeEstoqueComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaEstoqueModel>();
        }
    }
}
