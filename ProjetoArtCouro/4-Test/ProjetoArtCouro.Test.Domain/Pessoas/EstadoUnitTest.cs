using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class EstadoUnitTest
    {
        [TestMethod]
        public void ValidarEstadoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var estado = new Estado();
                estado.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "EstadoNome"))),
                    "Falta mensagem estado nome obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEstadoComPropriedadesObrigatoriasPreenchidas()
        {
            var estado = new Estado
            {
                EstadoNome = "sdas"
            };
            estado.Validar();
            Assert.AreEqual(estado.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarEstadoComEstadoNomeComMaisDe250Caracteres()
        {
            try
            {
                var estado = new Estado
                {
                    EstadoNome = new string('A', 251)
                };
                estado.Validar();
            }
            catch (Exception e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "EstadoNome", 250))),
                    "Falta mensagem estado nome mais de 250 caracteres");
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
