using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.Domain.Models.Usuario;
using System.Collections.Generic;
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

        [Given(@"que preencha os dados de configuracao do usuario com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDeConfiguracaoDoUsuarioComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<ConfiguracaoUsuarioModel>();
        }

        [Given(@"que preencha os dados das permissoes do usuario com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDasPermissoesDoUsuarioComAsSeguintesInformacoes(Table table)
        {
            Assert.IsNotNull(_scenarioContext["Conteudo"], "É necessário preencher o ConfiguracaoUsuarioModel antes");
            var permissoes = (List<PermissaoModel>)table.CreateSet<PermissaoModel>();
            if (_scenarioContext["Conteudo"] as ConfiguracaoUsuarioModel != null)
            {
                ((ConfiguracaoUsuarioModel)_scenarioContext["Conteudo"]).Permissoes = permissoes;
                return;
            }
            ((GrupoModel)_scenarioContext["Conteudo"]).Permissoes = permissoes;
        }

        [Given(@"que preencha os dados do grupo com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoGrupoComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<GrupoModel>();
        }

        [Given(@"que preencha os dados do filtro de pesquisa de grupo com as seguintes informações:")]
        public void DadoQuePreenchaOsDadosDoFiltroDePesquisaDeGrupoComAsSeguintesInformacoes(Table table)
        {
            _scenarioContext["Conteudo"] = table.CreateInstance<PesquisaGrupoModel>();
        }
    }
}
