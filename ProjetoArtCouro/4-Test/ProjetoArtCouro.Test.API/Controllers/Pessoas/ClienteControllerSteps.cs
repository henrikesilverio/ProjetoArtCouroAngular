using ProjetoArtCouro.Domain.Models.Cliente;
using ProjetoArtCouro.Test.API.Infra;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Pessoas
{
    [Binding]
    public class ClienteControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ServiceRequest _serviceRequest;

        private ClienteModel ClienteModel
        {
            get => _scenarioContext["ClienteModel"] as ClienteModel;
            set => _scenarioContext["ClienteModel"] = value;
        }

        public ClienteControllerSteps(
            ScenarioContext scenarioContext,
            ServiceRequest serviceRequest)
        {
            _scenarioContext = scenarioContext;
            _serviceRequest = serviceRequest;
            _scenarioContext.Add("ClienteModel", null);
        }

        [Given(@"que preencha os dados do cliente com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoClienteComAsSeguintesInformacoes(Table table)
        {
            ClienteModel = table.CreateInstance<ClienteModel>();
        }

        [When(@"realizar uma chamada Post ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaPostAoEndereco(string endereco)
        {
            var response = _serviceRequest.Post(ClienteModel, endereco);
            _scenarioContext.Add("Response", response);
        }
    }
}
