using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.ContaReceber;
using ProjetoArtCouro.Test.API.Controllers.Contas;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API
{
    [Binding]
    public class ContaReceberControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ContaReceberControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new ContaTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do filtro de pesquisa de contas a receber com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeContasAReceberComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaContaReceberModel>();
        }
        
        [Given(@"que preencha os dados da contas a receber com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDaContasAReceberComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateSet<ContaReceberModel>();
        }
    }
}
