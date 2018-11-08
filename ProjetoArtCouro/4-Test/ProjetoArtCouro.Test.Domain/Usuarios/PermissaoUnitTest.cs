using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Usuarios
{
    [TestClass]
    public class PermissaoUnitTest
    {
        [TestMethod]
        public void ValidarPermissaoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var permissao = new Permissao();
                permissao.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 2);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "PermissaoNome"))),
                    "Falta mensagem nome da permissão obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "AcaoNome"))),
                    "Falta mensagem nome da ação obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPermissaoComPropriedadesObrigatoriasPreenchidas()
        {
            var permissao = new Permissao
            {
                AcaoNome = "Novo",
                PermissaoNome = "Novo"
            };
            permissao.Validar();
        }

        [TestMethod]
        public void ValidarPermissaoComNomeDaAcaoComMaisDe50Caracteres()
        {
            try
            {
                var permissao = new Permissao
                {
                    AcaoNome = new string('A', 51),
                    PermissaoNome = "Novo"
                };
                permissao.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "AcaoNome", 50))),
                    "Falta mensagem nome da ação com mais de 50 caracteres");
            }
        }

        [TestMethod]
        public void ValidarPermissaoComNomeDaPermissaoComMaisDe50Caracteres()
        {
            try
            {
                var permissao = new Permissao
                {
                    AcaoNome = "Novo",
                    PermissaoNome = new string('A', 51),
                };
                permissao.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "PermissaoNome", 50))),
                    "Falta mensagem nome da permissão com mais de 50 caracteres");
            }
        }
    }
}
