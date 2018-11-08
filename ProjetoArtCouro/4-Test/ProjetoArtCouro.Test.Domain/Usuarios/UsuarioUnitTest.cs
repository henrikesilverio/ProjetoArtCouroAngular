using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;
using System.Linq;

namespace ProjetoArtCouro.Test.Domain.Usuarios
{
    [TestClass]
    public class UsuarioUnitTest
    {
        [TestMethod]
        public void ValidarUsuarioSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var usuario = new Usuario();
                usuario.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 2);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "UsuarioNome"))),
                    "Falta mensagem nome do usuario obrigatório");

                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "Senha"))),
                    "Falta mensagem senha obrigatório");
            }
        }

        [TestMethod]
        public void ValidarUsuarioComPropriedadesObrigatoriasPreenchidas()
        {
            var usuario = new Usuario
            {
                UsuarioNome = "Usuario",
                Senha = "123"
            };
            usuario.Validar();
        }

        [TestMethod]
        public void ValidarUsuarioComNomeComMaisDe60Caracteres()
        {
            try
            {
                var usuario = new Usuario
                {
                    UsuarioNome = new string('A', 61),
                    Senha = "123"
                };
                usuario.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "UsuarioNome", 60))),
                    "Falta mensagem nome do usuario com mais de 60 caracteres");
            }
        }

        [TestMethod]
        public void ValidarUsuarioComSenhaComMaisDe32Caracteres()
        {
            try
            {
                var usuario = new Usuario
                {
                    UsuarioNome = "Usuario",
                    Senha = new string('A', 33),
                };
                usuario.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Senha", 32))),
                    "Falta mensagem senha com mais de 32 caracteres");
            }
        }
    }
}
