using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.FormaPagamento;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Pagamentos
{
    [Binding]
    public class FormaPagamentoControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public FormaPagamentoControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext.Add("Conteudo", null);
            _scenarioContext["TestShared"] = new FormaPagamentoTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados da forma de pagamento com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDaFormaDePagamentoComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<FormaPagamentoModel>();
        }
    }
}
