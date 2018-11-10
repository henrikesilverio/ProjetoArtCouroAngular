using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Test.Domain.Helpers;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class PapelUnitTest
    {
        [TestMethod]
        public void ValidarPapelSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var papel = new Papel();
                papel.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "PapelNome"))),
                    "Falta mensagem papel nome obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPapelComPropriedadesObrigatoriasPreenchidas()
        {
            var papel = new Papel
            {
                PapelNome = "sdasd"
            };
            papel.Validar();
            Assert.AreEqual(papel.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarPapelComPapelNomeComMaisDe250Caracteres()
        {
            try
            {
                var papel = new Papel
                {
                    PapelNome = new string('A', 251)
                };
                papel.Validar();
                Assert.Fail("Deveria retornar um erro");
            }
            catch (DomainException e)
            {
                var mensagens = TesteAuxiliar.ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "PapelNome", 250))),
                    "Falta mensagem papel nome com mais de 250 caracteres");
            }
        }
    }
}
