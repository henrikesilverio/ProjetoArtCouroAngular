using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Resources.Resources;

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
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
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
            }
            catch (Exception e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "PapelNome", 250))),
                    "Falta mensagem papel nome com mais de 250 caracteres");
            }
        }

        private static string[] ObterMensagensValidas(Exception e, int quantidadeDeMensagens)
        {
            Assert.AreNotEqual(e.Message, "", "Nao retornou mensagens");
            var mensagens = e.Message.Split('-');
            Assert.AreNotEqual(mensagens.Length, 0, "Nao retornou mensagens");
            Assert.AreEqual(mensagens.Length, quantidadeDeMensagens, "Quantidade de mensagens invalida");
            mensagens = mensagens.Select(x => x.Trim()).ToArray();
            return mensagens;
        }
    }
}
