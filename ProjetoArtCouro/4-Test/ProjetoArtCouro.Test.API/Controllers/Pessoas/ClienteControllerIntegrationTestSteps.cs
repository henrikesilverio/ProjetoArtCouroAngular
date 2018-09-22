using System;
using TechTalk.SpecFlow;

namespace ProjetoArtCouro.Test.API.Controllers.Pessoas
{
    [Binding]
    public class ClienteControllerIntegrationTestSteps
    {
        [Given(@"que inicie o teste")]
        public void DadoQueInicieOTeste()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
