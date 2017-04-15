using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class EstadoCivilUnitTest
    {
        [TestMethod]
        public void ValidarEstadoCivilSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var estadoCivil = new EstadoCivil();
                estadoCivil.Validar();
            }
            catch (InvalidOperationException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "EstadoCivilNome"))),
                    "Falta mensagem estado civil nome obrigatório");
            }
        }

        [TestMethod]
        public void ValidarEstadoCivilComPropriedadesObrigatoriasPreenchidas()
        {
            var estadoCivil = new EstadoCivil
            {
                EstadoCivilNome = "sdas"
            };
            estadoCivil.Validar();
            Assert.AreEqual(estadoCivil.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarEstadoCivilComEstadoCivilNomeComMaisDe250Caracteres()
        {
            try
            {
                var estadoCivil = new EstadoCivil
                {
                    EstadoCivilNome = new string('A', 251)
                };
                estadoCivil.Validar();
            }
            catch (Exception e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "EstadoCivilNome", 250))),
                    "Falta mensagem estado civil nome mais de 250 caracteres");
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
