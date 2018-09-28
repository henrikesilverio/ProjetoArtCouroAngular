using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
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

        public ClienteControllerSteps(
            ScenarioContext scenarioContext,
            ServiceRequest serviceRequest,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _serviceRequest = serviceRequest;
            _scenarioContext.Add("Conteudo", null);
            _scenarioContext["TestShared"] = new ClienteTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do cliente com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoClienteComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<ClienteModel>();
        }

        [Given(@"que preecha os dados do endereço do cliente com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDoEnderecoDoClienteComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o ClienteModel antes");
            ((ClienteModel)_scenarioContext["Conteudo"]).Endereco = table.CreateInstance<EnderecoModel>();
        }

        [Given(@"que preecha os dados de meios de comunicação do cliente com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDeMeiosDeComunicacaoDoClienteComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o ClienteModel antes");
            ((ClienteModel)_scenarioContext["Conteudo"]).MeioComunicacao = table.CreateInstance<MeioComunicacaoModel>();
        }

        [Given(@"que preencha os dados do filtro de pesquisa de cliente com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeClienteComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaClienteModel>();
        }

        [When(@"realizar uma chamada Post ao endereço '(.*)'")]
        public void QuandoRealizarUmaChamadaPostAoEndereco(string endereco)
        {
            var response = _serviceRequest.Post(_scenarioContext["Conteudo"], endereco);
            _scenarioContext.Add("Response", response);
        }
    }
}
