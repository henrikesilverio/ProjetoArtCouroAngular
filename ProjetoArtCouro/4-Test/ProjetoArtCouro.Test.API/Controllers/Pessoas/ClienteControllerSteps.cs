using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Models.Cliente;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Test.API.Infra;
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
            _scenarioContext["TestShared"] = new ClienteTestShared(_scenarioContext);
        }

        [Given(@"que preencha os dados do cliente com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoClienteComAsSeguintesInformacoes(Table table)
        {
            ClienteModel = table.CreateInstance<ClienteModel>();
        }

        [Given(@"que preecha os dados do endereço do cliente com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDoEnderecoDoClienteComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(ClienteModel, "É necessário preencher o ClienteModel antes");
            ClienteModel.Endereco = table.CreateInstance<EnderecoModel>();
        }

        [Given(@"que preecha os dados de meios de comunicação do cliente com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDeMeiosDeComunicacaoDoClienteComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(ClienteModel, "É necessário preencher o ClienteModel antes");
            ClienteModel.MeioComunicacao = table.CreateInstance<MeioComunicacaoModel>();
        }

        [When(@"realizar uma chamada Post ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaPostAoEndereco(string endereco)
        {
            var response = _serviceRequest.Post(ClienteModel, endereco);
            _scenarioContext.Add("Response", response);
        }
    }
}
