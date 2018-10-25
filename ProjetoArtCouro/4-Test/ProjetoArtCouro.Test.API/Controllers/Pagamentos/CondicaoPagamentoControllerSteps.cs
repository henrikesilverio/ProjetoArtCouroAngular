using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.CondicaoPagamento;
using ProjetoArtCouro.Test.API.Controllers.Pagamentos;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API
{
    [Binding]
    public class CondicaoPagamentoControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CondicaoPagamentoControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new CondicaoPagamentoTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados da condicao de pagamento com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDaCondicaoDePagamentoComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<CondicaoPagamentoModel>();
        }
    }
}
