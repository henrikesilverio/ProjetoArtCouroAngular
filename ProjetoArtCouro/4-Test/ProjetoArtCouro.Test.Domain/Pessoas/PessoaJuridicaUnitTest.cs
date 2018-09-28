using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Test.Domain.Pessoas
{
    [TestClass]
    public class PessoaJuridicaUnitTest
    {
        [TestMethod]
        public void ValidarPessoaJuridicaSemPropriedadesObrigatoriasPreenchidas()
        {
            try
            {
                var pessoaJuridica = new PessoaJuridica();
                pessoaJuridica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 2);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "CNPJ"))),
                    "Falta mensagem CNPJ obrigatório");
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyPerson)),
                    "Falta mensagem pessoa obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaJuridicaComPropriedadesObrigatoriasPreenchidas()
        {
            var pessoaJuridica = new PessoaJuridica
            {
                CNPJ = "sdasd",
                Pessoa = new Pessoa()
            };
            pessoaJuridica.Validar();
            Assert.AreEqual(pessoaJuridica.Notifications.Count, 0, "Existem mensagens de erros");
        }

        [TestMethod]
        public void ValidarPessoaJuridicaSemCNPJ()
        {
            try
            {
                var pessoaJuridica = new PessoaJuridica
                {
                    Pessoa = new Pessoa()
                };
                pessoaJuridica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldIsRequired, "CNPJ"))),
                    "Falta mensagem CNPJ obrigatório");
            }
        }

        [TestMethod]
        public void ValidarPessoaJuridicaComContatoComMaisDe100Caracteres()
        {
            try
            {
                var pessoaJuridica = new PessoaJuridica
                {
                    CNPJ = "sdasd",
                    Contato = new string('A', 101),
                    Pessoa = new Pessoa()
                };
                pessoaJuridica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(string.Format(Erros.FieldMustHaveMaxCharacters, "Contato", 100))),
                    "Falta mensagem Contato com mais de 100 caracteres");
            }
        }

        [TestMethod]
        public void ValidarPessoaJuridicaSemPessoa()
        {
            try
            {
                var pessoaJuridica = new PessoaJuridica
                {
                    CNPJ = "sdasd"
                };
                pessoaJuridica.Validar();
            }
            catch (DomainException e)
            {
                var mensagens = ObterMensagensValidas(e, 1);
                Assert.IsTrue(mensagens.Any(x => x.Contains(Erros.EmptyPerson)),
                    "Falta mensagem papel pessoa obrigatório");
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
