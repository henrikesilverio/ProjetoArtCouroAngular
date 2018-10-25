using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Produto;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Produtos
{
    [Binding]
    public class ProdutoControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ProdutoControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new ProdutoTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do produto com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoProdutoComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<ProdutoModel>();
        }
    }
}
