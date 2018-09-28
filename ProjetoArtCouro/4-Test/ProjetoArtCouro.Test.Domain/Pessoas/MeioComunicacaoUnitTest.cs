using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class MeioComunicacaoUnitTest
    {
        [TestMethod]
        public void ValidarMeioComunicacaoSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var meioComunicacao = new MeioComunicacao();
                meioComunicacao.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 2);
                Assert.IsTrue(
                    mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "MeioComunicacaoNome"))),
                    "Falta mensagem meio comunicação nome obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBe, "TipoComunicacao", 0))),
                    "Falta mensagem tipo comunicação obrigatório");
            }
        }

        [TestMethod]
        public void ValidarMeioComunicacaoComPropriedadesObrigatoriasPreenchidas()
        {
            var meioComunicacao = new MeioComunicacao
            {
                MeioComunicacaoNome = "dsads",
                Principal = true,
                TipoComunicacao = TipoComunicacaoEnum.Telefone
            };
            meioComunicacao.Validar();
            Assert.AreEqual(meioComunicacao.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarMeioComunicacaoSemMeioComunicacaoNome()
        {
            try
            {
                var meioComunicacao = new MeioComunicacao
                {
                    Principal = false,
                    TipoComunicacao = TipoComunicacaoEnum.Telefone
                };
                meioComunicacao.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(
                    mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "MeioComunicacaoNome"))),
                    "Falta mensagem meio comunicação nome obrigatório");
            }
        }


        [TestMethod]
        public void ValidarMeioComunicacaoComMeioComunicacaoNomeComMaisDe250Caracteres()
        {
            try
            {
                var meioComunicacao = new MeioComunicacao
                {
                    MeioComunicacaoNome = new string('A', 251),
                    Principal = false,
                    TipoComunicacao = TipoComunicacaoEnum.Telefone
                };
                meioComunicacao.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "MeioComunicacaoNome", 250))),
                    "Falta mensagem meio comunicação nome com mais de 250 caracteres");
            }
        }

        [TestMethod]
        public void ValidarMeioComunicacaoSemTipoComunicacao()
        {
            try
            {
                var meioComunicacao = new MeioComunicacao
                {
                    MeioComunicacaoNome = "sdas",
                    Principal = false
                };
                meioComunicacao.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldCannotBe, "TipoComunicacao", 0))),
                    "Falta mensagem tipo comunicação obrigatório");
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
