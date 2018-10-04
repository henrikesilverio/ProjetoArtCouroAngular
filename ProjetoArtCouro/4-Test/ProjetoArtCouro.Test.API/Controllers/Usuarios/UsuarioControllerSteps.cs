using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Usuario;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ProjetoArtCouro.Test.API.Controllers.Usuarios
{
    [Binding]
    public class UsuarioControllerSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public UsuarioControllerSteps(
            ScenarioContext scenarioContext,
            DataBaseContext context)
        {
            _scenarioContext = scenarioContext;
            _scenarioContext.Add("Conteudo", null);
            _scenarioContext["TestShared"] = new UsuarioTestShared(_scenarioContext, context);
        }

        [Given(@"que preencha os dados do usuario com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoUsuarioComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<UsuarioModel>();
        }

        [Given(@"que preencha os dados do filtro de pesquisa de usuario com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeUsuarioComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaUsuarioModel>();
        }

    }
}
