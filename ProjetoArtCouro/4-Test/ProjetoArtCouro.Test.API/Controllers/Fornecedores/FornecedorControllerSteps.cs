using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Fornecedor;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.Test.API.Controllers.Pessoas;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Fornecedores
{
    [Binding]
    public class FornecedorControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public FornecedorControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext["Conteudo"] = null;
            _scenarioContext["TestShared"] = new PessoaTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do fornecedor com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFornecedorComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<FornecedorModel>();
        }

        [Given(@"que preecha os dados do endereço do fornecedor com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDoEnderecoDoFornecedorComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o FornecedorModel antes");
            ((FornecedorModel)_scenarioContext["Conteudo"]).Endereco = table.CreateInstance<EnderecoModel>();
        }

        [Given(@"que preecha os dados de meios de comunicação do fornecedor com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDeMeiosDeComunicacaoDoFornecedorComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o FornecedorModel antes");
            ((FornecedorModel)_scenarioContext["Conteudo"]).MeioComunicacao = table.CreateInstance<MeioComunicacaoModel>();
        }

        [Given(@"que preencha os dados do filtro de pesquisa de fornecedor com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeFornecedorComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaPessoaModel>();
        }
    }
}
