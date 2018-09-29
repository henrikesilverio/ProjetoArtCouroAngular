using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Funcionario;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.Test.API.Controllers.Pessoas;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Funcionarios
{
    [Binding]
    public class FuncionarioControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public FuncionarioControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext.Add("Conteudo", null);
            _scenarioContext["TestShared"] = new PessoaTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do funcionario com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFuncionarioComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<FuncionarioModel>();
        }
        
        [Given(@"que preecha os dados do endereço do funcionario com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDoEnderecoDoFuncionarioComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o FuncionarioModel antes");
            ((FuncionarioModel)_scenarioContext["Conteudo"]).Endereco = table.CreateInstance<EnderecoModel>();
        }
        
        [Given(@"que preecha os dados de meios de comunicação do funcionario com as seguintes informações:")]
        public void DadoQuePreechaOsDadosDeMeiosDeComunicacaoDoFuncionarioComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o FuncionarioModel antes");
            ((FuncionarioModel)_scenarioContext["Conteudo"]).MeioComunicacao = table.CreateInstance<MeioComunicacaoModel>();
        }
        
        [Given(@"que preencha os dados do filtro de pesquisa de funcionario com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeFuncionarioComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaPessoaModel>();
        }
    }
}
